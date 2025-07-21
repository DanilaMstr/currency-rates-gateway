pipeline {
    agent any
    stages {
        stage('Build') {
            steps {
                sh 'dotnet restore'
                sh 'dotnet build --no-restore'
            }
        }
        stage('Test') { 
            steps {
                sh 'dotnet test --no-build --no-restore --collect "XPlat Code Coverage" --results-directory TestResults' 
            }
            post {
                always {
                    publishCobertura(
                        coberturaReportFile: '**/TestResults/**/coverage.cobertura.xml',
                        sourceEncoding: 'ASCII',
                        failUnhealthy: false,
                        failUnstable: false
                    )
                }
            }
        }
    }
}