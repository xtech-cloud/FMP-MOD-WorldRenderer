
@echo off

REM !!! Generated by the fmp-cli 1.70.0.  DO NOT EDIT!

md WorldRenderer\Assets\3rd\fmp-xtc-worldrenderer

cd ..\vs2022
dotnet build -c Release

copy fmp-xtc-worldrenderer-lib-mvcs\bin\Release\netstandard2.1\*.dll ..\unity2021\WorldRenderer\Assets\3rd\fmp-xtc-worldrenderer\