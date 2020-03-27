@echo off

set OrganizationName=%1
set RepoName=%2
set Sha=%3
set RelativePath=%4
set OutPath=%5
set OAuthToken=Put An OAuth Token Here 

echo Downloading %Sha%:%RelativePath% from github.com/%OrganizationName%/%RepoName% to %OutPath%
curl -H "Authorization: token %OAuthToken%" http://localhost:1234/GitHubSourceIndexResolver/%OrganizationName%/%RepoName%/%Sha%/%RelativePath% -o %OutPath%

