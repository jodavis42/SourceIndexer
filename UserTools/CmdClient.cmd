@echo off

set RepositoryType=%1
set RepositoryName=%2
set Sha=%3
set RelativePath=%4
set OutPath=%5

set RepoPath=""
IF %RepositoryName% == "AzureCrashProject" (
  echo Repository AzureCrashProject Downloading  %RelativePath%#%Sha% to %OutPath%
  set RepoPath="E:\Code\Repos\AzureCrashProject"
)

git.exe -C %RepoPath% show %Sha%:%RelativePath% > %OutPath%
