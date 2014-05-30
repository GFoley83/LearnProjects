//Compilation Process:
//1. Preprocessor - removes comments, handles #include directives, etc. --> outpus a file.
//2. Compiler - generates low-level assembly language code --> outputs a file.
//3. Assembler - takes this text file and generates object code which is simply several numbers that represent instructions to processor.
//4. Linker - brings together several object code files, either stand-alone or in libraries, and creates the final executable.

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

//Standard C Library
//<assert.h>	Assertion Checking
//<ctype.h>		Character Classification
//<errno.h>		Error Reporting
//<string.h>	Buffer Manipulation
//				strcpy, strcat (concatenate), strlen, strchr (find char), 
//				strstr (find str), strcmp, strtok (split on token).
//<setjmp.h>	Non-Local Jumps, error-handeling or multi-threated
//<signal.h>	Event Signalling
//<stdarg.h>	Variable-Length Argument Lists
//<time.h>		Time Functions
//<math.h>		Math Functions
//<stdio.h>		IO for files, keyboard and display

extern void GvL();
//extern void Arithmetic();
//extern void IfStatements();
//extern void NestedIf();
//extern void FindingForLoops();
//extern void FindingWhileLoops();
//extern int performAction();
//extern int checkResult(int);
extern void Caller();
extern int test(int, int, int);
//extern void PvM();
//extern int adder(int, int);
//extern void Arrays();
//extern testSTruct(struct my_struct);
//extern void callStruct();
extern void Pointers();
extern void DynamicMemory(int size);
//extern void DynamicMemory2D(int, int);
//extern void MemoryManipulation();
//extern void RemoveArrayHead();
//extern void UnallocatedStorage();
extern void StrBuffer();
extern int FuncExample(int, int);

int main(/*int argc, char *argv[]*/)
{
	//Unlike C++, C variables must be declared before the first statement.
	//int i;
	//for (i = 0; i < argc; i++)
	//{ 
	//	printf("Argument %d is %s\n", i, argv[i]);
	//}

	//GvL();
	//Pointers();
	//DynamicMemory(10);
	//StrBuffer();

	int sum = FuncExample(100, 200);

	//getchar();
	return 0;
}

int FuncExample(int a, int b)
{
	int c = a + b;
	return c;
}

//Gloval vs. Local
int Gx = 1;
int Gy = 2;
void GvL()
{
	//Local vars referenced from stack.
	int a = 100;
	int b = 200;
	int c; 
	c = a + b;
	printf("Total = %d\n", c);

	//Global vars referenced from memory addresses.
	Gx = Gx + Gy;
	printf("Total = %d\n", Gx);
}

//Function Call Conventions:
//*cdecl - most popular, param are pushed onto stack from right to left, caller cleans up stack, return value stoes in EAX.
//*stdcall - callee cleans up stack, used by windows system calls.
//*fastcall - similar to cdecl, first two arguments passed as EDX and ECX with additional arguments loaded right to left. 
void Caller()
{
	int a = 1, b = 2, c = 3, ret;
	ret = test(a, b, c);
	printf("ret=%d\n", ret);
}

int test(int x, int y, int z)
{
	int sum = x + y + z;
	return sum;
}

void Pointers()
{
	int k = 7;		
	int *ptr1;		//int pointer
	int *ptr2 = &k;	//int pointer
	int i[10];		//int array, size 10
	void *vptr;		//void pointer
	char my_string[40] = "Howdy";

	//pointer has address, value
	ptr1 = &k;		//ptr1 value = k address
	*ptr2 = k;		//ptr2 address = k value
	printf("%d %d\n", *ptr1, &ptr1);
	printf("%d %d\n", *ptr2, &ptr2);
}

//Dynamic Memory Allocation
//*malloc - memory allocation
//*calloc - like malloc, but also zero-outs data
//*realloc - attempts to take a pointer to previously allocated memory and resize the memory block.
void DynamicMemory(int size)
{
	int *iptr;
	int i;

	iptr = (int *)malloc(size * sizeof(int));

	for (i = 0; i < size; i++)
	{
		iptr[i] = i + 1;
		printf("%d %d\n", iptr[i], &iptr[i]);
	}

	printf("Size of iptr is %d\b", iptr[size-1]);
}

//String Buffers
void StrBuffer()
{
	//Must initialize myString with a memory size.
	char *myString = (char *)malloc(100);
	strcpy(myString, "Howdy World!");
	
	printf("%s?\n",myString);

	//strcmp returns = if true
	if (strcmp(myString, "Howdy World!") == 0)
		printf("strings are the same.");
	else
		printf("strings are not the same.");
	
	//frees memory
	free(myString);
}