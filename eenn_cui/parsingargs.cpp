#include "parsingargs.h"
#include <list>

ParsingArgs::ParsingArgs()
{
}

ParsingArgs::~ParsingArgs()
{
}

bool ParsingArgs::AddArgType(char shortName, const char * longName, KeyFlag flag)
{
	if (nullptr == longName && 0 == shortName)
	{
		return false;
	}
	Option tmp;
	tmp.m_longName = longName;
	tmp.m_shortName = shortName;
	tmp.m_flag = flag;
	m_args.push_back(tmp);
	return true;
}

ParsingArgs::KeyFlag ParsingArgs::GetKeyFlag(std::string &key)
{
	for (int i = 0; i<m_args.size(); ++i)
	{
		std::string shortName = "-";
		std::string longName = "--";
		shortName += m_args[i].m_shortName;
		longName += m_args[i].m_longName;
		if (0 == key.compare(shortName) ||
			(0 == key.compare(longName))
			)
		{
			RemoveKeyFlag(key);
			return m_args[i].m_flag;
		}
	}
	return INVALID_KEY;
}

void ParsingArgs::RemoveKeyFlag(std::string & word)
{
	if (word.size() >= 2)
	{
		if (word[1] == '-')
		{
			word.erase(1, 1);
		}
		if (word[0] == '-')
		{
			word.erase(0, 1);
		}
	}
}

bool ParsingArgs::GetWord(std::string & Paras, std::string & word)
{
	size_t iNotSpacePos = Paras.find_first_not_of(' ', 0);
	if (iNotSpacePos == std::string::npos)
	{
		Paras.clear();
		word.clear();
		return true;
	}
	int length = Paras.size();
	std::list<char> specialChar;
	int islashPos = -1;
	for (int i = iNotSpacePos; i<length; i++)
	{
		char cur = Paras[i];
		bool bOk = false;
		switch (cur)
		{
		case ' ':
			if (specialChar.empty())
			{
				if (i != (length - 1))
				{
					Paras = std::string(Paras, i + 1, length - i - 1);
				}
				else
				{
					Paras.clear();
				}
				bOk = true;
			}
			else
			{
				word.append(1, cur);
			}
			break;
		case '"':
			if (specialChar.empty())
			{
				specialChar.push_back(cur);
			}
			else if (specialChar.back() == cur)
			{
				specialChar.pop_back();
			}
			else
			{
				word.clear();
				return false;
			}
			break;
		default:
			word.append(1, Paras[i]);
			if (i == (length - 1))
			{
				bOk = true;
				Paras.clear();
			}
			break;
		}
		if (bOk)
		{
			return true;
		}
	}
	if (specialChar.empty())
	{
		Paras.clear();
		return true;
	}
	else
	{
		return false;
	}
}

bool ParsingArgs::IsDuplicateKey(const std::string &key, const std::map<std::string, std::vector<std::string> > & result)
{
	if (result.find(key) != result.end())
	{
		return true; 
	}

	for (int i = 0; i<m_args.size(); ++i)
	{
		if ((key.compare(m_args[i].m_longName) == 0 && result.find(std::string(1, m_args[i].m_shortName)) != result.end())
			|| (key.compare(std::string(1, m_args[i].m_shortName)) == 0 && result.find(m_args[i].m_longName) != result.end())
			)
		{
			return true;
		}
	}
	return false;
}

int ParsingArgs::Parse(const std::string & Paras, std::map<std::string, std::vector<std::string> > & result, std::string &errPos)
{
	std::string tmpString = Paras;
	KeyFlag keyFlag = INVALID_KEY;
	std::string sKey = ""; 
	bool bFindValue = false; 
	while (!tmpString.empty())
	{
		std::string word = "";

		bool bRet = GetWord(tmpString, word);

		if (bRet == false)
		{
			errPos = tmpString;
			return -4;
		}
		else
		{
			KeyFlag tmpFlag = GetKeyFlag(word);
			if (IsDuplicateKey(word, result))
			{
				errPos = tmpString;
				return -5;
			}
			if (tmpFlag != INVALID_KEY)
			{
				if (tmpFlag == MUST_VALUE && keyFlag == MUST_VALUE && !bFindValue)
				{
					errPos = tmpString;
					return -3;
				}
				keyFlag = tmpFlag;
				std::vector<std::string> tmp;
				result[word] = tmp;
				sKey = word;
				bFindValue = false;
			}
			else
			{
				switch (keyFlag)
				{
				case MAYBE_VALUE:
				case MUST_VALUE:
				{
					std::map<std::string, std::vector<std::string> >::iterator it = result.find(sKey);
					if (it != result.end())
					{
						it->second.push_back(word);
						bFindValue = true;
					}
					else
					{
						errPos = tmpString;
						return -1;
					}
				}
				break;
				case NO_VALUE:
					errPos = tmpString;
					return -2;
				default:
					errPos = tmpString;
					return -1;
				}
			}
		}
	}
	return 0;
}
