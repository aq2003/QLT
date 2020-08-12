using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_LexerTester
{
    public class gen_variable_record
    {
        public string name;
        public string value;
        //public string inner_name;

        public gen_variable_record(string vname, string vvalue)
        {
            name = vname;
            value = vvalue;
        }

        public override string ToString()
        {
            string res = name + "\t" + value + "\n";

            return res;
        }
    }


    class Q_Gen
    {
        Dictionary<string, gen_variable_record> variables = new Dictionary<string, gen_variable_record>();
        Int32 system_names_count = 0;

        List<string> gen_line = new List<string>();

        parcer_record[] parcer_line;

        public string[] get_line() { finalize();  return gen_line.ToArray(); }

        public Q_Gen(parcer_record[] parcer_l)
        {
            parcer_line = parcer_l;
            
            variables.Add("open", new gen_variable_record("#O", "p0"));
            variables.Add("close", new gen_variable_record("#C", "p0"));
            variables.Add("high", new gen_variable_record("#H", "p0"));
            variables.Add("low", new gen_variable_record("#L", "p0"));
            variables.Add("typical", new gen_variable_record("#T", "p0"));
            variables.Add("median", new gen_variable_record("#M", "p0"));
            variables.Add("time", new gen_variable_record("#X", "00:00_00.00.00"));
            variables.Add("volume", new gen_variable_record("#V", "p0"));
            variables.Add("account", new gen_variable_record("#A", "l0"));
            variables.Add("money", new gen_variable_record("#N", "p0"));
            variables.Add("equity", new gen_variable_record("#E", "p0"));
            variables.Add("start_equity", new gen_variable_record("#SE", "p0"));
            variables.Add("max_equity", new gen_variable_record("#XE", "p0"));
            variables.Add("min_equity", new gen_variable_record("#NE", "p0"));
            variables.Add("profit", new gen_variable_record("#SP", "%0"));
            variables.Add("max_profit", new gen_variable_record("#SX", "%0"));
            variables.Add("min_profit", new gen_variable_record("#SN", "%0"));
            variables.Add("pos.price", new gen_variable_record("#R", "p0"));
            variables.Add("pos.age", new gen_variable_record("#G", "c0"));
            variables.Add("pos.profit", new gen_variable_record("#P", "%0"));
            variables.Add("pos.abs_profit", new gen_variable_record("#AP", "p0"));
            //31.01.2016 Not supported variables.Add("pos.loss", new gen_variable_record("#S", "%0"));
            variables.Add("log.status", new gen_variable_record("#log_status", "b0"));
            variables.Add("signal.OpenLong", new gen_variable_record("#SOL", "n0"));
            variables.Add("signal.CloseLong", new gen_variable_record("#SCL", "n0"));
            variables.Add("signal.OpenShort", new gen_variable_record("#SOS", "n0"));
            variables.Add("signal.CloseShort", new gen_variable_record("#SCS", "n0"));
            // +++ 17.02.2017
            variables.Add("last.price", new gen_variable_record("#LR", "p0"));
            variables.Add("last.age", new gen_variable_record("#LG", "c0"));
            variables.Add("last.profit", new gen_variable_record("#LP", "%0"));
            variables.Add("last.abs_profit", new gen_variable_record("#LAP", "p0"));
            variables.Add("last.balance", new gen_variable_record("#LA", "l0"));
            // +++ 17.02.2017

            system_names_count = variables.Count();
        }

        public void finalize()
        {
            int i = variables.Count;
            while (i > system_names_count)
            {
                i--;
                string str = variables.Values.ElementAt(i).ToString();
                gen_line.Insert(0, variables.Values.ElementAt(i).ToString());
            }
        }

        public void gen(Int32 pos)
        {
            parcer_record r = parcer_line[pos];

            // Adding variables
            if (r.code == "=")
                if (!variables.ContainsKey(r.arg[0]))
                {
                    gen_variable_record var = new gen_variable_record("v_" + r.arg[0], "n0");
                    variables.Add(r.arg[0], var);
                }

            if (r.code == "..") variables.Add("v_" + r.name, new gen_variable_record("v_" + r.name, "e0"));

            if (r.code == "||") r.code = "e|";

            if (r.code == "-") r.code = "m-";

            // Changing variable names
            int j = parcer_record.arg_count;
            while (j-- > 0)
                if (variables.ContainsKey(r.arg[j])) r.arg[j] = variables[r.arg[j]].name;
                
            // Print parcer_line
            if (r.code == "+" | r.code == "m-" | r.code == "*" | r.code == "/" |
                r.code == ">" | r.code == "<" | r.code == ">=" | r.code == "<=" |
                r.code == "==" | r.code == "!=" | r.code == "#_" | r.code == "#^" |
                r.code == ">=" | r.code == "<=" | r.code == "&" | r.code == "|" |
                r.code == "=" | r.code == "+=" | r.code == "-=" | r.code == "*=" |
                r.code == "/=" | r.code == ".." | r.code == "e|")

                gen_line.Add(r.ToString(2));

            else if (r.code == "-")
            {
                r.code = "m-";
                gen_line.Add(r.ToString(2));
            }

            else if (r.code == "!" | r.code == "neg")
                gen_line.Add(r.ToString(1));

            else if (r.code == "long" | r.code == "short" | r.code == "log")
                gen_line.Add(r.ToString(1));

            else if (r.code == "stop")
                gen_line.Add(r.ToString(1));

            else if (r.code == "~")
                gen_line.Add(r.ToString(0));

            else if (r.code == "ind")
            {
                if (r.arg[0] == "sar") gen_line.Add(r.ToString(4));
                else if (r.arg[0] == "atr") gen_line.Add(r.ToString(2));
                else if (r.arg[0] == "macd") gen_line.Add(r.ToString(5));
                else if (r.arg[0] == "pricechannel") gen_line.Add(r.ToString(3));
                else gen_line.Add(r.ToString(5));
            }

            //else if (r.code == "error") gen_line.Add(r.condition + "\n");

            else gen_line.Add(r.ToString(5));
        }

    }
}
