(Get-Content TcOpen.sln)  | Where-Object {$_ -notmatch "(ARMT2)"} | Set-Content TcOpen_.sln
(Get-Content TcOpen_.sln)  | Where-Object {$_ -notmatch "(ARMV7)"} | Set-Content TcOpen_.sln
(Get-Content TcOpen_.sln) | Where-Object {$_ -notmatch "ReleaseDevelop"} | Set-Content TcOpen_.sln
(Get-Content TcOpen_.sln) | Where-Object {$_ -notmatch "Debug|Any CPU"} | Set-Content TcOpen_.sln
(Get-Content TcOpen_.sln) | Where-Object {$_ -notmatch "Release|Any CPU"} | Set-Content TcOpen_.sln


