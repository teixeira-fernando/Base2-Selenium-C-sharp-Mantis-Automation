pipeline {
  agent any
  stages {
    stage('Checkout code') {
      steps {
        git 'https://github.com/teixeira-fernando/Base2-Selenium-C-sharp-Mantis-Automation.git'
      }
     stage('Restore Nuget') {
      steps {
        bat 'https://github.com/teixeira-fernando/Base2-Selenium-C-sharp-Mantis-Automation.git'
      }
    stage('Tests') {
      steps {
        echo 'pipeline message'
      }
    }
  }
}
