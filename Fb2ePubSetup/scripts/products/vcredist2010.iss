// requires Windows 7, Windows 7 Service Pack 1, Windows Server 2003 Service Pack 2, Windows Server 2008, Windows Server 2008 R2, Windows Server 2008 R2 SP1, Windows Vista Service Pack 1, Windows XP Service Pack 3
// requires Windows Installer 3.1 or later
// requires Internet Explorer 5.01 or later
// http://www.microsoft.com/en-us/download/details.aspx?id=30679


[CustomMessages]
en.vcredist2010_title= Microsoft Visual C++ 2010 SP1 Redistributable Package (x86) 
ru.vcredist2010_title= –аспростран€емый пакет пакета обновлени€ 1 дл€ Microsoft Visual C++ 2010 (x86)

en.vcredist2010_size=4.8 MB
ru.vcredist2010_size=4,8 MB


;http://www.microsoft.com/globaldev/reference/lcid-all.mspx
en.vcredist2010_lcid=
;russian
ru.vcredist2010_lcid=/lcid 1049


en.vcredist2010_url =http://download.microsoft.com/download/C/6/D/C6D0FD4E-9E53-4897-9B91-836EBA2AACD3/vcredist_x86.exe
ru.vcredist2010_url =http://download.microsoft.com/download/C/6/D/C6D0FD4E-9E53-4897-9B91-836EBA2AACD3/vcredist_x86.exe


[Code]
const 
  VC_2010_INSTALL_PATH = 'SOFTWARE\Microsoft\VisualStudio\10.0\VC\VCRedist\x86\';

function VC2010VersionInstalled(): Boolean;
var
Installed: cardinal;
begin
  RegQueryDWordValue(HKEY_LOCAL_MACHINE_32,VC_2010_INSTALL_PATH ,'Installed',Installed);
  Result := (Installed = 1);
end;

function VCRedistNeedsInstall: Boolean;
begin
    // here the Result must be True when you need to install your VCRedist
  // or False when you don't need to, so now it's upon you how you build
  // this statement, the following won't install your VC redist only when
  Result :=  not VC2010VersionInstalled();
end;


procedure vcredist2010();
begin

	if (VCRedistNeedsInstall()) then
begin
		AddProduct('vcredist2010.exe',
			CustomMessage('vcredist2010_lcid') + '/passive /norestart',
			CustomMessage('vcredist2010_title'),
			CustomMessage('vcredist2010_size'),
			GetString(CustomMessage('vcredist2010_url'), CustomMessage('vcredist2010_url'), CustomMessage('vcredist2010_url')),
			false, false);
end;
end;