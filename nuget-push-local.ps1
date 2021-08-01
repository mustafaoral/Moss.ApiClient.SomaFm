if ($null -eq $env:moss_local_nuget_feed_path) {
    Write-Host "Environment variable 'moss_local_nuget_feed_path' missing"

    return;
}

$localFeedPath = $env:moss_local_nuget_feed_path

if ((Test-Path -Path $localFeedPath) -eq $false) {
    Write-Host "Path specified by environment variable 'moss_local_nuget_feed_path' doesn't exist: $localFeedPath"

    return
}

$versionSuffix = [string]::Format("{0:yyyyMMdd}.{1}", [DateTime]::Now, [Convert]::ToInt32(([DateTime]::Now - [DateTime]::Today).TotalSeconds))

dotnet clean --configuration Release
dotnet pack --configuration Release --verbosity Normal --version-suffix $versionSuffix

$filename = Get-ChildItem .\Moss.ApiClient.SomaFm\bin\Release\*$versionSuffix.nupkg | Select-Object Name
nuget add ".\Moss.ApiClient.SomaFm\bin\Release\$($filename.Name)" -s $localFeedPath

$filename = Get-ChildItem .\Moss.ApiClient.SomaFm.Extensions\bin\Release\*$versionSuffix.nupkg | Select-Object Name
nuget add ".\Moss.ApiClient.SomaFm.Extensions\bin\Release\$($filename.Name)" -s $localFeedPath
