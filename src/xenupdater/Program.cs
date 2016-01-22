﻿/* Copyright (c) Citrix Systems Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, 
 * with or without modification, are permitted provided 
 * that the following conditions are met:
 *
 * *   Redistributions of source code must retain the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer.
 * *   Redistributions in binary form must reproduce the above 
 *     copyright notice, this list of conditions and the 
 *     following disclaimer in the documentation and/or other 
 *     materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
 * MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE 
 * DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR 
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, 
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR 
 * SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS 
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING 
 * NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE 
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF 
 * SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XenUpdater
{
    class Program
    {
        static int Main(string[] args)
        {
            int returnvalue = -2;
            try
            {
                bool add = false;
                bool remove = false;
                bool check = true;

                // check params for config options...
                foreach (string arg in args)
                {
                    switch (arg.ToLower())
                    {
                        case "add":
                            add = true;
                            check = false;
                            break;
                        case "remove":
                            remove = true;
                            check = false;
                            break;
                        case "check":
                            check = true;
                            break;
                        default:
                            throw new ArgumentException(arg);
                    }
                }
                if (add && !remove && !check)
                {
                    using (Tasks tasks = new Tasks())
                    {
                        tasks.AddTask();
                    }
                    returnvalue = 0;
                }
                if (remove && !add && !check)
                {
                    using (Tasks tasks = new Tasks())
                    {
                        tasks.RemoveTask();
                    }
                    returnvalue =  0;
                }
                if (check && !add && !remove)
                {
                    AutoUpdate auto = new AutoUpdate();
                    returnvalue = auto.CheckNow();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print("Exception: " + e.Message);
                returnvalue = -1;
            }
            System.Diagnostics.Debug.Print("Returns: " + returnvalue.ToString());
            return returnvalue;
        }
    }
}
