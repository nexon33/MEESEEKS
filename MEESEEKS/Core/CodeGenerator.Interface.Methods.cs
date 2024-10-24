using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MEESEEKS.Models.CodeGeneration;
using MEESEEKS.Core.CodeGeneration.CRUD;

namespace MEESEEKS.Core
{
    public partial class CodeGenerator
    {
        private async Task<List<MemberDeclarationSyntax>> GenerateMethodsFromDescriptionAsync(CodeGenerationRequest request)
        {
            var methods = new List<MemberDeclarationSyntax>();

            if (request.Description.Contains("CRUD", StringComparison.OrdinalIgnoreCase))
            {
                var crudMethods = await CrudMethodGenerator.GenerateAllAsync();
                methods.AddRange(crudMethods);
            }

            return methods;
        }
    }
}
