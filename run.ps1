$startLocation = Get-Location
$projectLocation = "$startLocation\src\ImageProcessor\ImageProcessorWebapp\ImageProcessorWebapp.csproj"
$releaseLocation = "$startLocation\src\ImageProcessor\ImageProcessorWebapp\bin\Release\netcoreapp2.2\win-x64"
$productImagesWebLocation = "https://www.masterofmalt.com/external_resources/dev_interview/product_images.zip"

#Publish app
Write-Host "`n`nPublishing ImageProcessorWebapp to: $releaseLocation `n`n`r" -ForegroundColor Green
dotnet publish $projectLocation -c Release -r win-x64 --self-contained true

#Ensure product_images exists
Write-Host "`n`nWould you like to download and extract 'product_images' `n`rFrom --> '$productImagesWebLocation' `n`rTo --> '$releaseLocation\publish\'?" -ForegroundColor Green
Write-Host "Warning: product_images is roughly 240mb!" -ForegroundColor Red
$confirmation = Read-Host -Prompt "(y / n)"

if ($confirmation -eq 'y') {
    Write-Host "`nDownloading silently to: '$releaseLocation\publish\product_images' please wait... `n`rTakes ~2 minutes, go make a cuppa." -ForegroundColor Yellow
    $ProgressPreference = 'SilentlyContinue'
    Invoke-WebRequest -Uri $productImagesWebLocation -OutFile "$startLocation\product_images.zip"
    Expand-Archive "$startLocation\product_images.zip" -DestinationPath "$releaseLocation\publish\"
}
else
{
    Write-Host "`nPlease ensure the product images you wish to use can be found at: '$releaseLocation\publish\product_images'" -ForegroundColor Yellow
}

#Start app
Write-Host "`nStarting ImageProcessorWebapp: $releaseLocation\publish\ImageProcessorWebapp.exe" -ForegroundColor Green
set-location "$releaseLocation\publish\"
Start-Process ImageProcessorWebapp.exe
Start-Process http://localhost:5000
Set-Location $startLocation
Read-Host -Prompt "Press any key to exit"