COPYRIGHT = "Copyright 2011 Chris Edwards. All rights reserved."

require 'albacore'
include FileTest


CLR_TOOLS_VERSION = 'v4.0.30319'
BUILD_NUMBER_BASE = '1.0.0'
BUILD_CONFIG = ENV['BUILD_CONFIG'] || "Debug"
BUILD_CONFIG_KEY = ENV['BUILD_CONFIG_KEY'] || 'NET40'
BUILD_PLATFORM = ''
TARGET_FRAMEWORK_VERSION = (BUILD_CONFIG_KEY == "NET40" ? "v4.0" : "v3.5")

props = {
  :src => File.expand_path("src"),
  :output => File.expand_path("build_output"),
  :artifacts => File.expand_path("build_artifacts")
}

#task :default => [ :version, :build, :test ]
task :default => [ :clean, :compile, :test ]


Albacore.configure do |config|
  config.msbuild.use :net40
end

desc "Build"
msbuild :build => :version do |msb|
	msb.properties :Configuration => BUILD_CONFIG,
	    :BuildConfigKey => BUILD_CONFIG_KEY,
	    :TargetFrameworkVersion => TARGET_FRAMEWORK_VERSION,
	    :Platform => 'Any CPU'
	msb.properties[:TargetFrameworkVersion] = TARGET_FRAMEWORK_VERSION
	msb.use :net4 #MSB_USE
  msb.targets :Clean, :Build
  msb.solution = "src/Fluency.NET.sln"
end


desc "Update the common version information for the build. You can call this task without building."
assemblyinfo :version do |asm|
  asm_version = BUILD_NUMBER_BASE + ".0"
  build_number = "#{BUILD_NUMBER_BASE}.0"
  tc_build_number = ENV["BUILD_NUMBER"]
  build_number = "#{BUILD_NUMBER_BASE}.#{tc_build_number}" unless tc_build_number.nil?

  puts "Setting assembly file version to #{build_number}"
  
  asm.version = asm_version
  asm.file_version = build_number

  asm.description = "Fluency is designed to simplify the way you set up tests by acting as a factory to create object graphs populated with fake data. Fluency exposes a customizeable fluent interface that constructs test objects (including the full graph of dependencies). Yet it only requires you to specify the few things your test is actually concerned with--everything else is automatically populated with random (yet valid) data. Fluency comes complete with a very functional set of realistic random data generators that can be configured on a case-by-case basis or by convention to give you complete control over the randomness of your data."
  asm.company_name = "Chris Edwards"
  asm.product_name = "Fluency"
  asm.copyright = COPYRIGHT
  asm.custom_attributes :AssemblyInformationalVersion => "#{asm_version}",
	:ComVisibleAttribute => false,
	:CLSCompliantAttribute => false
  asm.output_file = "src/SolutionAssemblyInfo.cs"  
  asm.namespaces "System", "System.Reflection", "System.Runtime.InteropServices", "System.Security"
end

desc "Cleans, versions, compiles the application and generates build_output/."
task :compile => [:version, :build] do
	puts 'Copying dependencies to output folder'
	copyOutputFiles File.join(props[:src], "Fluency/bin/#{BUILD_CONFIG}"), "Fluency.{dll,pdb,xml}", props[:output]
end


desc "Prepares the working directory for a new build"
task :clean do
	FileUtils.rm_rf props[:artifacts]
	FileUtils.rm_rf props[:output]
	# work around latency issue where folder still exists for a short while after it is removed
	waitfor { !exists?(props[:output]) }
	waitfor { !exists?(props[:artifacts]) }

	Dir.mkdir props[:output]
	Dir.mkdir props[:artifacts]
end


mspec :test do |mspec|
  puts "Running Tests..."
  mspec.command = "src/packages/Machine.Specifications.0.4.24.0/tools/mspec-clr4.exe"
  mspec.assemblies File.join(props[:src], "Fluency.Tests/bin/#{BUILD_CONFIG}/Fluency.Tests.dll")
  mspec.html_output = props[:artifacts]
end


def copyOutputFiles(fromDir, filePattern, outDir)
	FileUtils.mkdir_p outDir unless exists?(outDir)
	Dir.glob(File.join(fromDir, filePattern)){|file|
		copy(file, outDir) if File.file?(file)
	}
end



def waitfor(&block)
	checks = 0

	until block.call || checks >10
		sleep 0.5
		checks += 1
	end

	raise 'Waitfor timeout expired. Make sure that you aren\'t running something from the build output folders, or that you have browsed to it through Explorer.' if checks > 10
end