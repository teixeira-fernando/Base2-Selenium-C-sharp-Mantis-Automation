pipeline {
  agent any
  stages {
    stage('Checkout code') {
      steps {
        git 'https://github.com/teixeira-fernando/Base2-Selenium-C-sharp-Mantis-Automation.git'
      }
     stage('Restore Nuget') {
      steps {
        bat 'C:/tools/nuget.exe restore Mantis-Automation/Mantis-Automation.sln'
      }
      stage('Build') {
      steps {
        bat "\"${tool 'MSBuild'}\" Mantis-Automation.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
      }
      stage('Tests') {
      steps {
        bat 'C:/tools/NUnit3.6/nunit3-console.exe SeleniumNUnitParam/bin/debug/SeleniumNUnitParam.dll'
      }
    stage('Tests') {
      steps {
        echo 'pipeline message'
      }
    }
  }
}
