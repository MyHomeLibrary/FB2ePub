
[CustomMessages]
en.installFFPlugin = Install Firefox browser "FB2 save to ePub" integration
ru.installFFPlugin = Установить интеграцию обозревателя FireFox с конвертером/сохранением FB2 в ePub

[Code]

// registry path to Firefox brench in registry
const 
  FFOX_REG_PATH = 'SOFTWARE\Mozilla\Mozilla Firefox';


  // Check if Firefox installed
function IsFirefoxInstalled : boolean;
begin
// the key should be located in 32 bit registy as FF is 32 bit app
if not RegKeyExists(HKEY_LOCAL_MACHINE_32,FFOX_REG_PATH) then 
begin
Result := false;  
end else
begin
Result := true;
end;
end;

