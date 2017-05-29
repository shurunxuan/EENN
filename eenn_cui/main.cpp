#include <iostream>
#include <eenn_utils.h>

#pragma comment(lib, "eenn_utils.lib")

int main(int argc, char** argv)
{
	std::string runPath(argv[0]);
	std::string base(runPath.begin(), runPath.begin() + runPath.rfind("\\"));
	std::string prototxt(base);
	std::string caffemodel(base);
	prototxt += "\\model\\deploy.prototxt";
	caffemodel += "\\model\\EENN.caffemodel";

	std::string inputFile(base);
	inputFile += "\\test.jpg";
	std::string outputFile(base);
	outputFile += "\\testout.png";

	std::cout << init_nvml() << std::endl;

	int hr = deploy(prototxt.c_str(), caffemodel.c_str(), inputFile.c_str(), outputFile.c_str(), true, 0, 41, true);
	

	system("pause");
	return 0;
}