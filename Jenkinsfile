pipeline {
  agent any
  stages {
    stage('Checkout code') {
      steps {
        git 'https://github.com/teixeira-fernando/Base2-Selenium-C-sharp-Mantis-Automation.git'
      }
    }
     stage('Restore Nuget') {
      steps {
        bat '"C:\\JenkinsTools\\NuGet.exe" restore '
      }
     }
      stage('Build') {
      steps {
        bat "\"${tool 'MSBuildLocal'}\" \"${"Mantis Automation.sln"}\" /p:Configuration=Debug /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"
      }
      }
      stage('Tests') {
      steps {   
        bat 'C:/JenkinsTools/tools/nunit3-console.exe "Mantis Automation/bin/Debug/Mantis Automation.dll"'
      }
      }
  }
  post {
        always {
            archiveArtifacts "Mantis Automation/bin/Debug/**"
            nunit testResultsPattern: 'TestResult.xml'
        }
    }
}
