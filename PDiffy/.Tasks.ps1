Include-PluginScripts

task Test NUnit, MSpec #, etc. >> enables composition of running all tests of different types under one task

task dev Clean, Compile, Test

task ci Clean, Compile, Test, OctoPack