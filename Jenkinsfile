node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
		bat 'nuget restore \"voltura tools.sln\"'
		bat "\"${tool 'MSBuild'}\" \"voltura tools.sln\" /p:Configuration=Release /p:Platform=\"x64\" /p:ProductVersion=1.2.0.${env.BUILD_NUMBER}"

	stage 'Archive'
		archive '\"Voltura Tools/bin/Release/**\"'

}
