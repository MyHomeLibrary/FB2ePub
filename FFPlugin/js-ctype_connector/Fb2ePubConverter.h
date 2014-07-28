#pragma once

class CFb2ePubConverter
{
public:
	CFb2ePubConverter(void);
	virtual ~CFb2ePubConverter(void);
	bool Convert(LPCWSTR inputPath,LPCWSTR outputPath);
};

