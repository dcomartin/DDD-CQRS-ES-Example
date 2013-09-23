param($rootPath, $toolsPath, $package, $project)

# Try to delete InstallationDummyFile.txt
if ($project) {
	$project.ProjectItems | ?{ $_.Name -eq "InstallationDummyFile.txt" } | %{ $_.Delete() }
}