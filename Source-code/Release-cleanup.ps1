#################################################
# Release cleanup for eMI User Controls Library #
#################################################

Write-Host "#################################################"
Write-Host "# Release cleanup for eMI User Controls Library #"
Write-Host "#################################################"
Write-Host

Write-Host "Removing the Visual Studio solution user options directory."
Write-Host

Remove-Item .\.vs -Force -Recurse

Write-Host "Removing the builds."
Write-Host

Remove-Item .\CloseableTabItem\Build -Force -Recurse
Remove-Item .\CloseableTabItemTesting\Build -Force -Recurse
Remove-Item .\FileSystemBrowserWindow\Build -Force -Recurse
Remove-Item .\FileSystemBrowserWindowTesting\Build -Force -Recurse

###########################
# Prompt for continuation #
###########################

Write-Host "Release cleanup completed. Press any key to continue." -nonewline

$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")