require 'bundler/setup'
require 'albacore'

ci_build = ENV['APPVEYOR_BUILD_VERSION'] ||= "0"
ci_commit = ENV['APPVEYOR_REPO_COMMIT'] ||= "0"

tool_nuget = 'tools/nuget/nuget.exe'
tool_xunit = 'tools/xunit/xunit.console.clr4.exe'

project_name = 'Finite'
project_version = "2.0.0"

project_output = 'build/bin'
package_output = 'build/deploy'

build_mode = ENV['mode'] ||= "Debug"

desc 'Restore nuget packages for all projects'
nugets_restore :restore do |n|
  n.exe = tool_nuget
  n.out = 'packages'
end

desc 'Set the assembly version number'
asmver :version do |v|

  v.file_path = "#{project_name}/Properties/AssemblyVersion.cs"
  v.attributes assembly_version: project_version,
               assembly_file_version: project_version,
               assembly_description: "Build: #{ci_build}, Commit Sha: #{ci_commit}"
end

desc 'Compile all projects'
build :compile do |msb|
  msb.target = [ :clean, :rebuild ]
  msb.prop 'configuration', build_mode
  msb.sln = "#{project_name}.sln"
end

desc 'Run all unit test assemblies'
test_runner :test do |xunit|
  xunit.exe = tool_xunit
  xunit.files = FileList['**/bin/*/*.tests.dll']
  xunit.add_parameter '/silent'
end

desc 'Build all nuget packages'
nugets_pack :pack do |n|

  FileUtils.mkdir_p(package_output) unless Dir.exists?(package_output)

  n.configuration = build_mode
  n.exe = tool_nuget
  n.out = package_output

  n.files = FileList["#{project_name}/*.csproj"]

  n.with_metadata do |m|
    m.description = 'A simple Finite State Machine library'
    m.authors = 'Andy Dote'
    m.project_url = 'https://github.com/pondidum/Finite'
    m.license_url = 'https://github.com/Pondidum/Finite/blob/master/LICENSE.txt'
    m.version = project_version
    m.tags = 'statemachine fsm graph'
  end

end

task :default => [ :restore, :version, :compile, :test ]
