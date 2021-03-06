﻿using System.Linq;
using System.Reflection;
using DbUp.Engine;
using DbUp.ScriptProviders;
using NUnit.Framework;

namespace DbUp.Specification
{
    public class EmbeddedScriptAndCodeProviderTests : SpecificationFor<EmbeddedScriptAndCodeProvider>
    {
        private SqlScript[] scriptsToExecute;

        public override EmbeddedScriptAndCodeProvider Given()
        {
            var assembly = Assembly.GetExecutingAssembly();

            return new EmbeddedScriptAndCodeProvider(assembly, s=>true);
        }

        public override void When()
        {
            scriptsToExecute = Subject.GetScripts(() => null).ToArray();
        }

        [Then]
        public void it_should_return_all_sql_files()
        {
            Assert.AreEqual(4, scriptsToExecute.Length);
        }

        [Then]
        public void should_provide_content_for_code_script()
        {
            Assert.AreEqual("test4", scriptsToExecute.Last().Contents);
        }
    }
}