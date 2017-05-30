#include <iostream>
#include <sstream>
#include <string>
#include <vector>
#include <eenn_utils.h>
#include "parsingargs.h"

#pragma comment(lib, "eenn_utils.lib")

void printHelp()
{
	std::string help = R"(
usage: eenn_cui -i INPUT_FILE -o OUTPUT_FILE [OPTIONS] [FLAGS]

	Options:
	-i, --input_file		Specify an input image
	-o, --output_file		Specify an output image
	-g, --gpu device_id		Use GPU device with device_id
					default with GPU:0			<OPTIONAL>
	-c, --cpu			Use CPU
					can't be used together with -g		<OPTIONAL>
	-s, --crop_size size		Specify crop size
					should be bigger than 20
					default is 64				<OPTIONAL>
	-l, --log			Use to output progress			<OPTIONAL>

	Example:
		eenn_cui -i "input image.jpg" -o output.png -g 0 -s 128 -l
)";
	std::cout << help;
}

int main(int argc, char** argv)
{
	std::vector<std::string> argList;
	std::string argstr;
	for (int i = 1; i < argc; ++i)
	{
		argList.push_back(argv[i]);
		argstr += argv[i];
		argstr += ' ';
	}

	ParsingArgs parser;
	parser.AddArgType('i', "input_file", ParsingArgs::MUST_VALUE);
	parser.AddArgType('o', "output_file", ParsingArgs::MUST_VALUE);
	parser.AddArgType('g', "gpu", ParsingArgs::MAYBE_VALUE);
	parser.AddArgType('c', "cpu", ParsingArgs::NO_VALUE);
	parser.AddArgType('s', "crop_size", ParsingArgs::MAYBE_VALUE);
	parser.AddArgType('l', "log", ParsingArgs::NO_VALUE);

	std::map<std::string, std::vector<std::string>> result;
	std::string errorPos;

	int error = parser.Parse(argstr, result, errorPos);

	//for (std::map<std::string, std::vector<std::string>>::iterator it = result.begin(); it != result.end(); ++it) {
	//	std::string key = it -> first;
	//	std::vector<std::string> value = it->second;
	//	std::cout << key << "\t";
	//	for (std::string val : value)
	//	std::cout << val << " ";
	//	std::cout << std::endl;
	//}
	//std::cout << "Error: " << error << "\tError Position: " << errorPos << std::endl;

	if (error < 0)
	{
		printHelp();
		return -1;
	}

	if (result.find("i") == result.end() && result.find("input_file") == result.end())
	{
		printHelp();
		return -1;
	}

	if (result.find("o") == result.end() && result.find("output_file") == result.end())
	{
		printHelp();
		return -1;
	}

	if ((result.find("c") != result.end() || result.find("cpu") != result.end())
		&& (result.find("g") != result.end() || result.find("gpu") != result.end()))
	{
		printHelp();
		return -1;
	}

	std::string inputFile;
	std::string outputFile;
	bool useGpu;
	int gpuDevice;
	int cropSize;

	if (result.find("i") != result.end())
		inputFile = result["i"][0];
	else
		inputFile = result["input_file"][0];

	if (result.find("o") != result.end())
		outputFile = result["o"][0];
	else
		outputFile = result["output_file"][0];

	if (result.find("g") != result.end())
	{
		useGpu = true;
		if (result["g"].size() == 1)
		{
			std::stringstream ss;
			ss << result["g"][0];
			ss >> gpuDevice;
		}
		else
			gpuDevice = 0;
	}
	else if (result.find("gpu") != result.end())
	{
		useGpu = true;
		if (result["gpu"].size() == 1)
		{
			std::stringstream ss;
			ss << result["gpu"][0];
			ss >> gpuDevice;
		}
		else
			gpuDevice = 0;
	}
	else if ((result.find("c") != result.end() || result.find("cpu") != result.end()))
	{
		useGpu = false;
		gpuDevice = 0;
	}
	else
	{
		useGpu = true;
		gpuDevice = 0;
	}

	if (result.find("s") != result.end())
	{
		std::stringstream ss;
		ss << result["s"][0];
		ss >> cropSize;
		if (cropSize <= 20)
		{
			printHelp();
			return -1;
		}
	}
	else if (result.find("crop_size") != result.end())
	{
		std::stringstream ss;
		ss << result["crop_size"][0];
		ss >> cropSize;
		if (cropSize <= 20)
		{
			printHelp();
			return -1;
		}
	}
	else
		cropSize = 64;

	bool log = (result.find("l") != result.end() || result.find("log") != result.end());

	std::string prototxt("model\\deploy.prototxt");
	std::string caffemodel("model\\EENN.caffemodel");

	int hr = deploy(prototxt.c_str(), caffemodel.c_str(), inputFile.c_str(), outputFile.c_str(), useGpu, gpuDevice, cropSize, log);

	return 0;
}