// eenn_utils.cpp : Defines the exported functions for the DLL application.
//
#define GLOG_NO_ABBREVIATED_SEVERITIES 
#include <string>
#include <iostream>
#include <fstream>
#include <cmath>
#include <nvml.h>
#include <cuda_runtime_api.h>
#include <caffe/caffe.hpp>
#include <caffe/data_transformer.hpp>
#include <opencv2/opencv.hpp>
#include "caffe_reg.h"

#include "eenn_utils.h"

// caffe
#pragma comment(lib, "caffe.lib")
#pragma comment(lib, "caffeproto.lib")
// gflags
#pragma comment(lib, "gflags.lib")
// glog
#pragma comment(lib, "glog.lib")
// nvml
#pragma comment(lib, "nvml.lib")
// cuda
#pragma comment(lib, "cuda.lib")
#pragma comment(lib, "cublas.lib")
#pragma comment(lib, "cudart.lib")
#pragma comment(lib, "curand.lib")
// boost
#pragma comment(lib, "libboost_thread-vc140-mt-1_61.lib")
#pragma comment(lib, "libboost_python-vc140-mt-1_61.lib")
// hdf5
#pragma comment(lib, "caffehdf5.lib")
#pragma comment(lib, "caffehdf5_hl.lib")
#pragma comment(lib, "caffehdf5_cpp.lib")
#pragma comment(lib, "caffehdf5_hl_cpp.lib")
// protobuf
#pragma comment(lib, "libprotobuf.lib")
// blas
#pragma comment(lib, "libopenblas.dll.a")
// opencv
#pragma comment(lib, "opencv_core310.lib")
#pragma comment(lib, "opencv_highgui310.lib")
#pragma comment(lib, "opencv_imgcodecs310.lib")
#pragma comment(lib, "opencv_imgproc310.lib")

// Check the unregistered layer
CAFFE_REG_H

// This is an example of an exported variable
float percentage = 0;

// This is an example of an exported function.
EENN_UTILS_API void set_progress(float value)
{
	percentage = value;
}

EENN_UTILS_API float get_progress()
{
	return percentage;
}

EENN_UTILS_API int output_string(char* str)
{
	std::string s(str);
	std::cout << str << std::endl;
	return s.size();
}

EENN_UTILS_API bool init_nvml()
{
	auto result = nvmlInit();
	return result == NVML_SUCCESS;
}

EENN_UTILS_API int get_gpu_count()
{
	unsigned int device_count = 0;
	auto result = nvmlDeviceGetCount(&device_count);
	if (result != NVML_SUCCESS)
		device_count = -1;
	return device_count;
}

EENN_UTILS_API char* get_gpu_name(unsigned int index)
{
	nvmlDevice_t device;
	auto result = nvmlDeviceGetHandleByIndex(index, &device);
	auto name = new char[NVML_DEVICE_NAME_BUFFER_SIZE];
	if (NVML_SUCCESS != result) return "Error!";
	result = nvmlDeviceGetName(device, name, NVML_DEVICE_NAME_BUFFER_SIZE);
	if (NVML_SUCCESS != result) return "Error!";
	return name;
}

EENN_UTILS_API unsigned int get_gpu_slowdown_temperature(unsigned int index)
{
	nvmlDevice_t device;
	auto result = nvmlDeviceGetHandleByIndex(index, &device);
	if (NVML_SUCCESS != result) return 0;
	unsigned int temp;
	result = nvmlDeviceGetTemperatureThreshold(device, NVML_TEMPERATURE_THRESHOLD_SLOWDOWN, &temp);
	if (NVML_SUCCESS != result) return 0;
	return temp;
}

EENN_UTILS_API unsigned int get_gpu_shutdown_temperature(unsigned int index)
{
	nvmlDevice_t device;
	auto result = nvmlDeviceGetHandleByIndex(index, &device);
	if (NVML_SUCCESS != result) return 0;
	unsigned int temp;
	result = nvmlDeviceGetTemperatureThreshold(device, NVML_TEMPERATURE_THRESHOLD_SHUTDOWN, &temp);
	if (NVML_SUCCESS != result) return 0;
	return temp;
}

EENN_UTILS_API unsigned int get_gpu_temperature(unsigned int index)
{
	nvmlDevice_t device;
	auto result = nvmlDeviceGetHandleByIndex(index, &device);
	if (NVML_SUCCESS != result) return 0;
	unsigned int temp;
	nvmlDeviceGetTemperature(device, NVML_TEMPERATURE_GPU, &temp);
	if (NVML_SUCCESS != result) return 0;
	return temp;
}

EENN_UTILS_API unsigned int get_gpu_utilization(unsigned int index)
{
	nvmlDevice_t device;
	auto result = nvmlDeviceGetHandleByIndex(index, &device);
	if (NVML_SUCCESS != result) return 0;
	nvmlUtilization_t utilization;
	nvmlDeviceGetUtilizationRates(device, &utilization);
	if (NVML_SUCCESS != result) return 0;
	return utilization.gpu;
}

EENN_UTILS_API unsigned int get_gpu_memory_usage(unsigned int index)
{
	nvmlDevice_t device;
	auto result = nvmlDeviceGetHandleByIndex(index, &device);
	if (NVML_SUCCESS != result) return 0;
	nvmlUtilization_t utilization;
	nvmlDeviceGetUtilizationRates(device, &utilization);
	if (NVML_SUCCESS != result) return 0;
	return utilization.memory;
}

void caffe_forward(boost::shared_ptr<caffe::Net<float>> & net, float *data_ptr)
{
	auto input_blobs = net->input_blobs()[0];
	switch (caffe::Caffe::mode())
	{
	case caffe::Caffe::CPU:
		memcpy(input_blobs->mutable_cpu_data(), data_ptr,
			sizeof(float) * input_blobs->count());
		break;
	case caffe::Caffe::GPU:
		cudaMemcpy(input_blobs->mutable_gpu_data(), data_ptr,
			sizeof(float) * input_blobs->count(), cudaMemcpyHostToDevice);
		break;
	default:
		LOG(FATAL) << "Unknown Caffe mode.";
	}
	net->Forward();
}

void caffe_forward(boost::shared_ptr<caffe::Net<float>> & net, cv::Mat& block, caffe::DataTransformer<float>& input_xformer)
{
	auto input_blobs = net->input_blobs()[0];
	input_xformer.Transform(block, input_blobs);
	switch (caffe::Caffe::mode())
	{
	case caffe::Caffe::CPU:
		memcpy(input_blobs->mutable_cpu_data(), input_blobs->cpu_data(),
			sizeof(float) * input_blobs->count());
		break;
	case caffe::Caffe::GPU:
		cudaMemcpy(input_blobs->mutable_gpu_data(), input_blobs->cpu_data(),
			sizeof(float) * input_blobs->count(), cudaMemcpyHostToDevice);
		break;
	default:
		LOG(FATAL) << "Unknown Caffe mode.";
	}
	net->Forward();
}

cv::Mat DatumToCvMat(const caffe::Datum& datum)
{
	int img_type;
	switch (datum.channels())
	{
	case 1:
		img_type = CV_8UC1;
		break;
	case 2:
		img_type = CV_8UC2;
		break;
	case 3:
		img_type = CV_8UC3;
		break;
	default:
		img_type = CV_8U;
		CHECK(false) << "Invalid number of channels.";
		break;
	}

	cv::Mat mat(datum.height(), datum.width(), img_type);
	//  cvCreateData( &mat );

	//  CvMat* mat_p = cvCreateMat( datum.height(), datum.width(), img_type );
	auto datum_channels = datum.channels();
	auto datum_height = datum.height();
	auto datum_width = datum.width();

	for (auto h = 0; h < datum_height; ++h) {
		auto ptr = mat.ptr<uchar>(h);
		auto img_index = 0;
		for (auto w = 0; w < datum_width; ++w) {
			for (auto c = 0; c < datum_channels; ++c) {
				auto datum_index = (c * datum_height + h) * datum_width + w;
				float datum_float_val = datum.float_data(datum_index);
				if (datum_float_val >= 255.0)
				{
					ptr[img_index++] = 255;
				}
				else if (datum_float_val <= 0.0)
				{
					ptr[img_index++] = 0;
				}
				else
				{
					ptr[img_index++] = static_cast<uchar>(lrint(datum_float_val));
				}
			}
		}
	}
	return mat;
}

EENN_UTILS_API int deploy(const char* proto, const char* model, const char* input, const char* output, bool using_gpu, unsigned int gpu_device, unsigned int crop_size, bool output_log)
{
	auto input_size = crop_size;
	auto output_size = crop_size - 20;
	if (output_size <= 0)
	{
		return -1;
	}
	auto difference = input_size - output_size;

	std::string prototxt(proto);
	std::string temptxt(prototxt.begin(), prototxt.begin() + prototxt.rfind('.'));
	std::string caffemodel(model);
	temptxt += "temp.prototxt";

	std::ifstream originalPrototxt(prototxt);
	std::ofstream outputPrototxt(temptxt);
	std::string line;

	for (auto i = 0; i < 4; ++i)
	{
		getline(originalPrototxt, line);
		outputPrototxt << line << std::endl;
	}
	for (auto i = 0; i < 2; ++i)
	{
		getline(originalPrototxt, line);
		outputPrototxt << "  dim: " << input_size << std::endl;
	}
	while (getline(originalPrototxt, line))
		outputPrototxt << line << std::endl;

	outputPrototxt.close();
	originalPrototxt.close();


	if (using_gpu)
	{
		caffe::Caffe::set_mode(caffe::Caffe::GPU);
		caffe::Caffe::SetDevice(gpu_device);
	}
	else
	{
		caffe::Caffe::set_mode(caffe::Caffe::CPU);
	}

	boost::shared_ptr<caffe::Net<float>> net(new caffe::Net<float>(temptxt, caffe::TEST));
	net->CopyTrainedLayersFrom(caffemodel);

	caffe::TransformationParameter input_xform_param;
	input_xform_param.add_mean_value(133);
	input_xform_param.add_mean_value(128);
	input_xform_param.add_mean_value(138);
	caffe::DataTransformer<float> input_xformer(input_xform_param, caffe::TEST);

	caffe::TransformationParameter output_xform_param;
	caffe::DataTransformer<float> output_xformer(output_xform_param, caffe::TEST);

	std::string imgPath(input);		// Input image path
	std::string imgOutPath(output);

	auto img = cv::imread(imgPath);								// Input image
	cv::Mat img2x;													// Image resized to target size with bicubic interpolation

	auto height = img.rows * 2;										// Target height
	auto width = img.cols * 2;										// Target width
	
																	// Resize the input image
	resize(img, img2x, cv::Size(width, height), 0, 0, CV_INTER_LINEAR);

	int new_width = int(ceil(width / float(output_size)) * output_size) + difference;	// Width for fill
	int new_height = int(ceil(height / float(output_size)) * output_size) + difference;	// Height for fill

	cv::Mat imgFit = cv::Mat::zeros(new_height, new_width, CV_8UC3);					// Image with border
	cv::Rect fit(difference / 2, difference / 2, width, height);						// Image ROI in border
	auto fitROI = imgFit(fit);														// Image in border
	img2x.convertTo(fitROI, fitROI.type());

	cv::Mat imgReconstruct = cv::Mat::zeros(new_height, new_width, CV_8UC3);			// Reconstructed image with border

	for (auto i = 0; i < width; i += output_size)
	{
		for (auto j = 0; j < height; j += output_size)
		{
			percentage = float(i) / new_width * 100 + float(j) / new_width / new_height * 100;
			LOG_IF_EVERY_N(INFO, output_log, 50) << percentage << "% Done";
			cv::Rect roi(i, j, input_size, input_size);
			auto block = imgFit(roi);

			// Process block
			caffe_forward(net, block, input_xformer);
			auto raw_blob_ptr = net->output_blobs()[0];

			caffe::Blob<float> output_blob;
			output_blob.Reshape(raw_blob_ptr->num(), raw_blob_ptr->channels(),
				raw_blob_ptr->height(), raw_blob_ptr->width());

			output_xformer.Transform(raw_blob_ptr, &output_blob);
			output_blob.Reshape(raw_blob_ptr->num(), block.channels(),
				block.rows, block.cols);

			// Convert the output blob back to an image.
			caffe::Datum datum;
			datum.set_height(output_blob.height());
			datum.set_width(output_blob.width());
			datum.set_channels(output_blob.channels());
			datum.clear_data();
			datum.clear_float_data();
			auto blob_data = output_blob.cpu_data();

			for (auto m = 0; m < output_blob.count(); ++m)
			{
				datum.add_float_data(blob_data[m]);
			}

			auto mat = DatumToCvMat(datum);


			cv::Rect outputROI(difference / 2, difference / 2, output_size, output_size);
			auto blockOutput = mat(outputROI);

			cv::Rect reconstructROI(i + difference / 2, j + difference / 2, output_size, output_size);
			auto reconstructBlock = imgReconstruct(reconstructROI);
			blockOutput.convertTo(reconstructBlock, CV_8UC3);
		}
	}
	percentage = 100.00f;
	LOG_IF(INFO, output_log) << percentage << "% Done";

	auto imgReconstructContent = imgReconstruct(fit);
	cv::Mat imgOutput;
	imgReconstructContent.convertTo(imgOutput, CV_8UC3);
	imwrite(imgOutPath, imgOutput);
	return 0;
}
