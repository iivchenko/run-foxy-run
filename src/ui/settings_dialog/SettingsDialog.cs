using Godot;

public class SettingsDialog : Control
{
    [Signal]
    public delegate void BackButtonPressed();

    private const string MusicBus = "Music";
    private const string SfxBux = "Master";

    private HSlider _musicVolumeSlider;
    private HSlider _sfxVolumeSlider;
    private Button _backButton;

    private int _musicBusIndex = -1;
    private int _sfxBusIndex = -1;


    public override void _Ready()
    {
        _musicVolumeSlider = FindNode("MusicVolume") as HSlider;
        _sfxVolumeSlider = FindNode("SfxVolume") as HSlider;

        _backButton = FindNode("BackButton") as Button;

        _musicBusIndex = AudioServer.GetBusIndex(MusicBus);
        _sfxBusIndex = AudioServer.GetBusIndex(SfxBux);

        _musicVolumeSlider.Value = GD.Db2Linear(AudioServer.GetBusVolumeDb(_musicBusIndex));
        _sfxVolumeSlider.Value = GD.Db2Linear(AudioServer.GetBusVolumeDb(_sfxBusIndex));

        _musicVolumeSlider.Connect("value_changed", this, nameof(OnMusicVolumeChanged));
        _sfxVolumeSlider.Connect("value_changed", this, nameof(OnAudioVolumeChanged));

        _backButton.Connect("pressed", this, nameof(OnBackButtonPressed));
        _backButton.GrabFocus();

    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioServer.SetBusVolumeDb(_musicBusIndex, GD.Linear2Db(value));
    }

    private void OnAudioVolumeChanged(float value)
    {
        AudioServer.SetBusVolumeDb(_sfxBusIndex, GD.Linear2Db(value));
    }

    private void OnBackButtonPressed()
    {
        EmitSignal(nameof(BackButtonPressed));
    }
}
