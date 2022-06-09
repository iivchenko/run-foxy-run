$gVersion = "3.4.4"

$url = "https://downloads.tuxfamily.org/godotengine/$gVersion/mono/Godot_v$gVersion-stable_mono_win64.zip"
$serverurl="https://downloads.tuxfamily.org/godotengine/3.4.4/Godot_v3.4.4-stable_linux_headless.64.zip"
$out = ".tools"

function Download-Godot {

    New-Item -ItemType Directory -Force -Path $out
    Invoke-WebRequest -Uri $url -OutFile "$out/godot.zip"
    expand-archive -path "$out/godot.zip" -DestinationPath "$out/"
    Remove-Item "$out/godot.zip"
}

function Download-SeverGodot {
    New-Item -ItemType Directory -Force -Path $out
    Invoke-WebRequest -Uri $serverurl -OutFile "./$out/godot.zip" -Verbose
    expand-archive -path "./$out/godot.zip" -DestinationPath "./$out/"
    Remove-Item "$./out/godot.zip"
}

function Run-Editor {
    
    param()

    if (-not (Test-Path -Path "$out/Godot_v$gVersion-stable_mono_win64/"))
    {
        Download-Godot
    }

    & "$out/Godot_v$gVersion-stable_mono_win64/Godot_v$gVersion-stable_mono_win64.exe" --editor --path ./src
}

function Run-Game {

    param()

    if (-not (Test-Path -Path "$out/godot.exe"))
    {
        Download-Godot
    }

    & "$out/godot.exe"
}

function Run-ServerExport {
     param([string]$target)
	
    if (-not (Test-Path -Path "$out/Godot_v$gVersion-stable_linux_headless.64"))
    {
        Download-SeverGodot
    }

    & "$out/Godot_v$gVersion-stable_linux_headless.64" --export $target --path ./src
}

Export-ModuleMember -Function * -Alias *