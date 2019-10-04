# Image Processor

## How to run
* Download and extract this repo
* Open the root folder and run the powershell script `run.ps1`
* Follow the prompts in the powershell script, the script will:
  * Publish the webapp project with it's dependencies
  * Ask if you'd like the `product_images` to be downloaded and extracted into the correct place (this is optional as it's a 240mb download, if you select `n` it'll prompt you were to put the images manually)
  * Start up the webapp and your default browser to `http://localhost:5000`
 
 
![powershell](https://user-images.githubusercontent.com/35634732/64322579-bb9ce880-cfba-11e9-83ac-0ef00350c18d.png)


![start up](https://user-images.githubusercontent.com/35634732/64374678-4104b500-d01c-11e9-9e45-fc15ae6d3acc.png)


![chrome](https://user-images.githubusercontent.com/35634732/64374747-5974cf80-d01c-11e9-8e64-69fe9b52a576.png)


---

## How to use
Localhost:5000 should provide you a simple page to pick images and make adjustments. When you're happy with your selection just hit the `Request Image` button. The link provided can be clicked to open in a new window, the query string isn't shortened in any way and made easy to change to request different variations.


![image request](https://user-images.githubusercontent.com/35634732/64374810-77423480-d01c-11e9-89c5-cca2628ddedf.png)


![image request 2](https://user-images.githubusercontent.com/35634732/64374839-8a550480-d01c-11e9-88cc-55d0eb3c6c93.png)

