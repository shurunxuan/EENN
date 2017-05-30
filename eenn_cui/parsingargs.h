#pragma once
#ifndef PARSINGARGS_H
#define PARSINGARGS_H

#include <map>
#include <vector>
#include <string>

class ParsingArgs
{
public:
	ParsingArgs();
	~ParsingArgs();
	enum KeyFlag { INVALID_KEY = -1, NO_VALUE, MAYBE_VALUE, MUST_VALUE };

	bool AddArgType(const char shortName, const char * longName = nullptr, KeyFlag flag = NO_VALUE);

	int Parse(const std::string & paras, std::map<std::string, std::vector<std::string> > & result, std::string &errPos);

private:

	KeyFlag GetKeyFlag(std::string &key);

	void RemoveKeyFlag(std::string & paras);

	bool GetWord(std::string & Paras, std::string & word);

	bool IsDuplicateKey(const std::string &key, const std::map<std::string, std::vector<std::string> > & result);

	struct Option
	{
		std::string m_longName;
		char m_shortName;
		KeyFlag m_flag;
	};

	std::vector<Option> m_args; 
};

#endif
