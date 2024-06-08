using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ColorNote_Backup_Viewer.Model
{
    enum state
    {
        obj,
        array,
        key,
        value_string,
        value_numeric,
        value_specialLetter
    }

    public class MemoJSONParser
    {
        StringBuilder newStr = new StringBuilder();

        public string[] parse(char[] rawData)
        {
            newStr.Clear();
            Stack<state> stateS = new Stack<state>();
            int classStep = 0;
            bool escape = false;
            char lastchar = '\0';

            List<string> result = new List<string>();

            foreach (char c in rawData)
            {
                if (stateS.Count == 0)
                {
                    if (c == '{')
                    {
                        newStr.Append(c);
                        stateS.Push(state.obj);
                        classStep += 1;
                    }
                }
                else 
                {
                    if (stateS.Peek() == state.obj)
                    {
                        if (c == '"')
                        {
                            newStr.Append(c);

                            if (lastchar == ':')
                                stateS.Push(state.value_string);
                            else
                            {
                                stateS.Push(state.key);
                                escape = false;
                            }
                        }
                        else if (c == ':' || c == ',')
                        {
                            newStr.Append(c);

                            lastchar = c;
                        }
                        else if (c == '[')
                        {
                            newStr.Append(c);

                            stateS.Push(state.array);
                            classStep += 1;
                        }
                        else if (c == '{')
                        {
                            newStr.Append(c);

                            stateS.Push(state.obj);
                            classStep += 1;
                            lastchar = '\0';
                        }
                        else if (c == '}')
                        {
                            newStr.Append(c);

                            stateS.Pop();
                            classStep -= 1;
                            lastchar = '\0';

                            if (classStep == 0)
                            {
                                result.Add(newStr.ToString());
                                newStr.Clear();
                            }
                        }
                        else if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'))
                        {
                            newStr.Append(c);

                            stateS.Push(state.value_specialLetter);
                        }
                        else if (('0' <= c && c <= '9') || c == '.')
                        {
                            newStr.Append(c);

                            stateS.Push(state.value_numeric);
                        }
                    }
                    else if (stateS.Peek() == state.array)
                    {
                        if (c == '"')
                        {
                            newStr.Append(c);

                            stateS.Push(state.value_string);
                        }
                        else if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'))
                        {
                            newStr.Append(c);

                            stateS.Push(state.value_specialLetter);
                        }
                        else if (('0' <= c && c <= '9') || c == '.')
                        {
                            newStr.Append(c);

                            stateS.Push(state.value_numeric);
                        }
                        else if (c == ',')
                        {
                            newStr.Append(c);
                        }
                        else if (c == '{')
                        {
                            newStr.Append(c);

                            stateS.Push(state.obj);
                            classStep += 1;
                            lastchar = '\0';
                        }
                        else if (c == '[')
                        {
                            newStr.Append(c);

                            stateS.Push(state.array);
                            classStep += 1;
                        }
                        else if (c == ']')
                        {
                            newStr.Append(c);

                            stateS.Pop();
                            classStep -= 1;
                            lastchar = '\0';
                        }
                    }
                    else if (stateS.Peek() == state.key || stateS.Peek() == state.value_string)
                    {
                        newStr.Append(c);
                        if (escape)
                            escape = false;
                        else if (c == '\\')
                            escape = true;
                        else if (c == '"')
                        {
                            stateS.Pop();
                            lastchar = '\0';
                        }
                    }
                    else if (stateS.Peek() == state.value_numeric)
                    {
                        if (('0' <= c && c <= '9') || c == '.')
                            newStr.Append(c);
                        else if (c == '}' || c == ']' || c == ',')
                        {
                            newStr.Append(c);

                            stateS.Pop();
                            if (c != ',')
                            {
                                stateS.Pop();
                                classStep -= 1;

                                if (classStep == 0)
                                {
                                    result.Add(newStr.ToString());
                                    newStr.Clear();
                                }
                            }
                            lastchar = '\0';
                        }
                        else
                        {
                            stateS.Pop();
                            lastchar = '\0';
                        }
                    }
                    else if (stateS.Peek() == state.value_specialLetter)
                    {
                        if (('a' <= c && c <= 'z') || ('A' <= c && c <= 'Z'))
                            newStr.Append(c);
                        else if (c == '}' || c == ']' || c == ',')
                        {
                            newStr.Append(c);

                            stateS.Pop();
                            if (c != ',')
                            {
                                stateS.Pop();
                                classStep -= 1;

                                if (classStep == 0)
                                {
                                    result.Add(newStr.ToString());
                                    newStr.Clear();
                                }
                            }
                            lastchar = '\0';
                        }
                        else
                        {
                            stateS.Pop();
                            lastchar = '\0';
                        }
                    }
                }
            }

            return result.ToArray();
        }
    }
}
