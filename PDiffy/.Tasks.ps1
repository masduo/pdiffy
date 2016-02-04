Include-PluginScripts

#bundle all test tasks here
task Test NUnit, MSpec

#composes the basic tasks
task basics Clean, Compile, Test

#dev should run locally, providing the variables are set
task dev variables, basics

task variables
{

}

#ci should run on build servers
task ci basics, Version, OctoPack, Push, OctoCreateRelease