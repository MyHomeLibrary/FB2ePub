// requires Windows 7, Windows 7 Service Pack 1, Windows Server 2003 Service Pack 2, Windows Server 2008, Windows Server 2008 R2, Windows Server 2008 R2 SP1, Windows Vista Service Pack 1, Windows XP Service Pack 3
// requires Windows Installer 3.1 or later
// requires Internet Explorer 5.01 or later
// http://www.microsoft.com/en-us/download/details.aspx?id=30679


[CustomMessages]
en.vcredist2012_title=Visual C++ Redistributable for Visual Studio 2012 Update 4 
ru.vcredist2012_title=Распространяемый пакет Visual C++ для Visual Studio 2012 Обновление 4 

en.vcredist2012_size=6.3 MB
ru.vcredist2012_size=6,3 MB

en.vcredist2012_size_x64=6.9 MB
ru.vcredist2012_size_x64=6,9 MB

;http://www.microsoft.com/globaldev/reference/lcid-all.mspx
en.vcredist2012_lcid=
;russian
ru.vcredist2012_lcid=/lcid 1049


; install , then search in C:\ProgramData\Package Cache\ for file name, subfolder name is GUID for package to detect
; mind no spaces after =
;11.0.61030 (Update4)
en.VC_2012_REDIST_X86_UP4 ={33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}
ru.VC_2012_REDIST_X86_UP4 ={f0080ca2-80ae-4958-b6eb-e8fa916d744a}



en.VC_2012_REDIST_X64_UP4 ={ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}
ru.VC_2012_REDIST_X64_UP4 ={a2199617-3609-410f-a8e8-e8806c73545b}


en.vcredist2012_u4_url =http://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU4/vcredist_x86.exe
ru.vcredist2012_u4_url =http://download.microsoft.com/download/E/C/C/ECCD0A46-78BF-4580-804C-CE0B61CF921E/VSU4/vcredist_x86.exe

en.vcredist2012_u4_url_x64 =http://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU4/vcredist_x64.exe
ru.vcredist2012_u4_url_x64 =http://download.microsoft.com/download/E/C/C/ECCD0A46-78BF-4580-804C-CE0B61CF921E/VSU4/vcredist_x64.exe

[Code]
const 
  VC_2012_UNINSTALL_PATH = 'SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\';
  VC_2012_REDIST_X86_UP4_GUID='{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}';
  VC_2012_REDIST_X64_UP4_GUID='{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}';

function VC2012VersionInstalled(const ProductID: string): Boolean;
var
Installed: cardinal;
begin
  RegQueryDWordValue(HKEY_LOCAL_MACHINE_32,VC_2012_UNINSTALL_PATH + ProductID ,'Installed',Installed);
  Result := (Installed = 1);
end;

function VCRedistNeedsInstall86: Boolean;
var localInstalled: Boolean;
var englishInstalled: Boolean;
begin
    // here the Result must be True when you need to install your VCRedist
  // or False when you don't need to, so now it's upon you how you build
  // this statement, the following won't install your VC redist only when
  localInstalled := VC2012VersionInstalled(CustomMessage('VC_2012_REDIST_X86_UP4'));
  englishInstalled := VC2012VersionInstalled(VC_2012_REDIST_X86_UP4_GUID);
  Result :=  not ( localInstalled or englishInstalled);
end;

function VCRedistNeedsInstall64: Boolean;
var localInstalled: Boolean;
var englishInstalled: Boolean;
begin
    // here the Result must be True when you need to install your VCRedist
  // or False when you don't need to, so now it's upon you how you build
  // this statement, the following won't install your VC redist only when
  localInstalled := VC2012VersionInstalled(CustomMessage('VC_2012_REDIST_X64_UP4'));
  englishInstalled := VC2012VersionInstalled(VC_2012_REDIST_X64_UP4_GUID);
  Result :=  ( (not ( localInstalled or englishInstalled)) and IsX64());
end;




procedure vcredist2012();
begin

	if (VCRedistNeedsInstall86()) then
begin
		AddProduct('vcredist2012' + GetArchitectureString() + '.exe',
			CustomMessage('vcredist2012_lcid') + '/passive /norestart',
			CustomMessage('vcredist2012_title')  + ' (x86)',
			CustomMessage('vcredist2012_size'),
			GetString(CustomMessage('vcredist2012_u4_url'), CustomMessage('vcredist2012_u4_url'), CustomMessage('vcredist2012_u4_url')),
			false, false);
end;
	if (VCRedistNeedsInstall64()) then
begin
		AddProduct('vcredist2012' + '.exe',
			CustomMessage('vcredist2012_lcid') + '/passive /norestart',
			CustomMessage('vcredist2012_title')+ ' (x64)',
			CustomMessage('vcredist2012_size_x64'),
			GetString(CustomMessage('vcredist2012_u4_url_x64'), CustomMessage('vcredist2012_u4_url_x64'), CustomMessage('vcredist2012_u4_url_x64')),
			false, false);
end ;
end;