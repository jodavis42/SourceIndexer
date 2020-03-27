@echo off

set RepositoryType=%1
set RepoUrl=%2
set Sha=%3
set RelativePath=%4
set OutPath=%5
set OAuthToken=Put An OAuth Token Here 

echo Downloading %Sha%:%RelativePath% from %OAuthToken% to %OutPath%
curl -H "Authorization: token %OAuthToken%" %RepoUrl%/%Sha%/%RelativePath% -o %OutPath%

