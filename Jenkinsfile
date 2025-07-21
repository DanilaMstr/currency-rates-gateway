pipeline {
    agent any
    options {
        skipStagesAfterUnstable()
    }
    stages {
        stage('Build') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build --no-restore'
            }
        }
        stage('Test') {
            steps {
                sh 'dotnet test --no-build --no-restore --collect "XPlat Code Coverage"'
            }
        }
        stage('Deliver') { 
            steps {
                sh 'dotnet publish CurrencyRatesGateway.API --no-restore -o published'  
            }
            post {
                success {
                    archiveArtifacts 'published/*.*' 
                }
            }
        }
    }
}