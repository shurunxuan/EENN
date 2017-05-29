// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the EENN_UTILS_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// EENN_UTILS_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#pragma once
#ifdef EENN_UTILS_EXPORTS
#define EENN_UTILS_API __declspec(dllexport)
#else
#define EENN_UTILS_API /*__declspec(dllimport)*/
#endif

EENN_UTILS_API void set_progress(float value);
EENN_UTILS_API float get_progress();

EENN_UTILS_API int deploy(const char* proto, const char* model, const char* input, const char* output, bool using_gpu, unsigned int gpu_device, unsigned int crop_size, bool output_log);

EENN_UTILS_API bool init_nvml();
EENN_UTILS_API int get_gpu_count();
EENN_UTILS_API char* get_gpu_name(unsigned int index);
EENN_UTILS_API unsigned int get_gpu_slowdown_temperature();
EENN_UTILS_API unsigned int get_gpu_shutdown_temperature();
EENN_UTILS_API unsigned int get_gpu_temperature();
EENN_UTILS_API unsigned int get_gpu_utilization();
EENN_UTILS_API unsigned int get_gpu_memory_usage();