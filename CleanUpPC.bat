echo off
FOR /r Dokumentation %%f IN (Master.pdf) DO del %%f
FOR /r Dokumentation %%f IN (*.aux) DO del %%f
FOR /r Dokumentation %%f IN (*.log) DO del %%f
FOR /r Dokumentation %%f IN (*.out) DO del %%f
FOR /r Dokumentation %%f IN (*.DS_Store) DO del %%f
FOR /r Dokumentation %%f IN (*.gz) DO del %%f
FOR /r Dokumentation %%f IN (*.toc) DO del %%f

FOR /r Dokumentation %%r IN (Thumbs.db) DO del %%f

FOR /r Rapport %%f IN (Master.pdf) DO del %%f
FOR /r Rapport %%f IN (*.aux) DO del %%f
FOR /r Rapport %%f IN (*.log) DO del %%f
FOR /r Rapport %%f IN (*.out) DO del %%f
FOR /r Rapport %%f IN (*.DS_Store) DO del %%f
FOR /r Rapport %%f IN (*.gz) DO del %%f
FOR /r Rapport %%f IN (*.toc) DO del %%f

FOR /r Rapport %%r IN (Thumbs.db) DO del %%f
echo on