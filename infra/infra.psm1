$gVersion = "3.4.4"

$url = "https://downloads.tuxfamily.org/godotengine/$gVersion/mono/Godot_v$gVersion-stable_mono_win64.zip"
$serverurl="https://downloads.tuxfamily.org/godotengine/3.4.4/Godot_v3.4.4-stable_linux_headless.64.zip"
$out = "$PSScriptRoot/../.tools"

function Download-Godot {

    New-Item -ItemType Directory -Force -Path $out
    wget -Uri $url -OutFile "$out/godot.zip"
    expand-archive -path "$out/godot.zip" -DestinationPath "$out/"
    Remove-Item "$out/godot.zip"
}

function Download-SeverGodot {
	New-Item -ItemType Directory -Force -Path $out
    Invoke-WebRequest -Uri $serverurl -OutFile "$out/godot.zip"
    expand-archive -path "$out/godot.zip" -DestinationPath $out 
    Rename-Item -Path "$out/Godot_v3.4.4-stable_linux_headless.64"  -NewName "godot"
    Remove-Item "$out/godot.zip"
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

function Run-Export {
    param()

    if (-not (Test-Path -Path "$out/godot.exe"))
    {
        Download-Godot
        
    }

    New-Item -ItemType Directory -Force -Path .\publish\winx64\

    & "$out\godot.exe" --path . --export "win-x64" .\publish\winx64\zobmies.exe
}

function Run-ServerExport {
    param()
        
	Download-SeverGodot
	
    New-Item -ItemType Directory -Force -Path publish/winx64

    & "$out\godot" --path . --export "win-x64" publish/winx64/zobmies.exe
}

function Run-Tests {
    param()

    if (-not (Test-Path -Path "$out\godot.exe"))
    {
        Download-Godot        
    }

    & "$out\godot.exe" -d --path . test\gut.tscn
}

Export-ModuleMember -Function * -Alias *