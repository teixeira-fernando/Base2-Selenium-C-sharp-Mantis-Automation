# Base2-Selenium-C-sharp-Mantis-Automation
Mantis website automation using C# + Selenium. Includes a CI pipeline configured with Jenkins

Some imprtant informations:

1 - Pipeline Configuration: https://resources.github.com/articles/practical-guide-to-CI-with-Jenkins-and-GitHub/
 * I configured a project in Jenkins that searches for a pipeline archive in my repository and then makes the build process acording to this file.

2 - Mantis Installation: It was installed a version of mantis bug tracker to make possible the automated tests execution. I used the Wamp Server software (http://www.wampserver.com/en/) to host the mantis website. It also provides the necessary database resources by using MySql. The Mantis's version used in this project was the 1.3.14 (It is available in the root of this repository).  
