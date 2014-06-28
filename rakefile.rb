MSBUILD_PATH = "C:/Windows/Microsoft.NET/Framework/v4.0.30319/msbuild.exe"
PATH_7ZIP = "C:/Program Files/7-Zip/7z.exe"
MSPEC_PATH = "tools/Machine.Specifications.ManualBuild/mspec-clr4.exe"
MSTEST_PATH = File.join(ENV['VS110COMNTOOLS'], '..', 'IDE', 'mstest.exe')
BUILD_PATH = File.expand_path('build')
DATABASE_DEPLOYMENT_PATH = File.expand_path('database_deployment')
DEPLOY_PATH = File.expand_path('deploy')
REPORTS_PATH = File.expand_path('testresults')
TEST_RESULTS_PATH = File.expand_path('testresults')
SOLUTION = "InvioApiClient.sln"
SOLUTION_PATH = File.join("src",SOLUTION)
TRXFILE = File.join(REPORTS_PATH, SOLUTION + '.trx')
CONFIG = "Debug"

task :default => [:all]

task :all => [:removeArtifacts, :compile]

task :removeArtifacts do
	require 'fileutils'
	FileUtils.rm_rf BUILD_PATH
	FileUtils.rm_rf REPORTS_PATH
	FileUtils.rm_rf DEPLOY_PATH
	FileUtils.rm_rf DATABASE_DEPLOYMENT_PATH	
end

task :compile do
	puts 'Compiling solution...'
	sh "#{MSBUILD_PATH} /p:Configuration=#{CONFIG} /p:OutDir=\"#{BUILD_PATH}/\" /p:PostBuildEvent=\"\" #{SOLUTION_PATH}"	
end