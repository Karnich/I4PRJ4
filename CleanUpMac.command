#!/bin/bash

cd "`dirname "$0"`"

find ./Dokumentation -name "*.log" -type f -delete
find ./Dokumentation -name "*.out" -type f -delete
find ./Dokumentation -name "*.gz" -type f -delete
find ./Dokumentation -name "*.toc" -type f -delete
find ./Dokumentation -name "*.aux" -type f -delete
find ./Dokumentation -name "Master.pdf" -type f -delete
find ./Dokumentation -name "Thumbs.db" -type f -delete

find ./Rapport -name "*.log" -type f -delete
find ./Rapport -name "*.out" -type f -delete
find ./Rapport -name "*.gz" -type f -delete
find ./Rapport -name "*.toc" -type f -delete
find ./Rapport -name "*.aux" -type f -delete
find ./Rapport -name "Master.pdf" -type f -delete
find ./Rapport -name "Thumbs.db" -type f -delete

osascript -e 'tell application "Terminal" to quit' &
exit