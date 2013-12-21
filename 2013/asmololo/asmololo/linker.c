#include "common.h"

extern Command *commands;
extern Label *symbolicLabels;
extern int commandsCount;
extern int labelsCount;

int linkLabel(char* s)
{
    int i;
    for (i = 0; i < labelsCount; i++)
    {
        if (!strcmp(symbolicLabels[i].name, s))
        {
            FREE(s);
            return symbolicLabels[i].commandNumber;
        }
    }
    error(ET_UNDEFINED_LABEL);
    return -1;

}

void linkProgram()
{
    int i;
    for (i = 0; i < commandsCount; i++)
    {
        switch (commands[i].type)
        {
        case CT_JMP:
            commands[i].args.arg1.argv.mappedLabel
                    = linkLabel(commands[i].args.arg1.argv.label);
            break;
        case CT_JE: case CT_JNE:
        case CT_JL: case CT_JNL:
        case CT_JG: case CT_JNG:
            commands[i].args.arg2.argv.mappedLabel
                    = linkLabel(commands[i].args.arg2.argv.label);
            break;
        default:
            break;
        }
    }
    for (i = 0; i < labelsCount; i++)
    {
        FREE(symbolicLabels[i].name);
    }
    FREE(symbolicLabels);
}
