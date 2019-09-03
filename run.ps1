$startLocation = Get-Location
$projectLocation = "$startLocation\src\ImageProcessor\ImageProcessorWebapp\ImageProcessorWebapp.csproj"
$releaseLocation = "$startLocation\src\ImageProcessor\ImageProcessorWebapp\bin\Release\netcoreapp2.2\win-x64"
$productImagesWebLocation = "https://www.masterofmalt.com/external_resources/dev_interview/product_images.zip"

#Publish app
Write-Host "Publishing ImageProcessorWebapp to: $releaseLocation" -ForegroundColor Magenta
dotnet publish $projectLocation -c Release -r win-x64 --self-contained true

#Ensure product_images exists
Write-Host "Would you like to download and extract 'product_images' from '$productImagesWebLocation' to '$startLocation\product_images'" -ForegroundColor Magenta
Write-Host "product_images is roughly 240mb, this will take a few minutes!!" -ForegroundColor Red
$confirmation = Read-Host "(y/ n)"
if ($confirmation -eq 'y') {
    Invoke-WebRequest -Uri $productImagesWebLocation -OutFile "$startLocation\product_images.zip"
    Expand-Archive "$startLocation\product_images.zip" -DestinationPath $startLocation
}
else
{
    Write-Host "Please ensure the product images you wish to use can be found at: '$startLocation\product_images'" -ForegroundColor Magenta
}

#Start app
Write-Host "Starting ImageProcessorWebapp" -ForegroundColor Green
Start-Process "$releaseLocation\ImageProcessorWebapp.exe"
Start-Process http://localhost:5000
Set-Location $startLocation