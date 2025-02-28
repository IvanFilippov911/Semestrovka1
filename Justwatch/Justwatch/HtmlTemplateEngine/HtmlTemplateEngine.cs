using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Justwatch.Templates;

public class TemplateGenerator
{
    public string Render(string template, Dictionary<string, object> data)
    {
        try
        {
            template = ProcessCondition(template, data);
            template = ProcessForeach(template, data);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return data
            .Where(kvp => kvp.Value is string)
            .Aggregate(template, (current, kvp) =>
                current.Replace($"{{{{{kvp.Key}}}}}", kvp.Value.ToString()));
    }

    private static string ProcessCondition(string template, Dictionary<string, object> data)
    {
        var conditionPattern = @"{{#if\s+(\w+)}}[\r\n]*\s*(.*?)[\r\n]*\s*{{\/if}}";
        var matches = Regex.Matches(template, conditionPattern);

        if (matches.Count == 0)
        {
            return template;
        }

        foreach (Match match in matches)
        {
            var condition = match.Groups[1].Value;
            var value = match.Groups[2].Value;

            template = data.ContainsKey(condition) && data[condition] is bool conditionValue && conditionValue
                ? template.Replace(match.Value, value)
                : template.Replace(match.Value, "");
        }

        return template;
    }
    //private string ProcessForeach(string template, Dictionary<string, object> data)
    //{
    //    var conditionPattern = @"{{#foreach\s+(\w+)\s+in\s+(\w+)}}([\s\S]*?){{/foreach}}";
    //    var matches = Regex.Matches(template, conditionPattern);
    //    if (matches.Count == 0)
    //    {
    //        return template;
    //    }

    //    foreach (Match match in matches)
    //    {
    //        string itemVar = match.Groups[1].Value;
    //        string enumerableVar = match.Groups[2].Value;
    //        string innerTemplate = match.Groups[3].Value;

    //        if (!data.ContainsKey(enumerableVar))
    //            throw new Exception($"Переменная перечисления '{enumerableVar}' не найдена в контексте.");

    //        if (!(data[enumerableVar] is IEnumerable enumerable))
    //            throw new Exception($"Переменная '{enumerableVar}' не является перечислением элементов.");

    //        var resultBuilder = new StringBuilder();

    //        // Обрабатываем каждый элемент в коллекции
    //        foreach (var item in enumerable)
    //        {
    //            // Для каждого элемента создаем копию шаблона
    //            string currentItemTemplate = innerTemplate;

    //            // Паттерн для замены свойств объекта
    //            var propPattern = $"{{{{\\s*{itemVar}\\.(\\w+)\\s*}}}}";
    //            var propMatches = Regex.Matches(currentItemTemplate, propPattern);

    //            foreach (Match propMatch in propMatches)
    //            {
    //                string propName = propMatch.Groups[1].Value;
    //                var itemType = item.GetType();
    //                var propertyInfo = itemType.GetProperty(propName);

    //                if (propertyInfo == null)
    //                    throw new Exception($"Свойство '{propName}' не найдено у объекта типа '{itemType.Name}'.");

    //                var propertyValue = propertyInfo.GetValue(item);
    //                currentItemTemplate = propertyValue == null
    //                        ? currentItemTemplate.Replace(propMatch.Value, "")
    //                        : currentItemTemplate.Replace(propMatch.Value, propertyValue.ToString() ?? "");
    //            }

    //            // Добавляем обработанный элемент в результат
    //            resultBuilder.Append(currentItemTemplate);
    //        }

    //        // Заменяем блок {{#foreach ...}} на все обработанные элементы
    //        template = template.Replace(match.Value, resultBuilder.ToString());
    //    }

    //    return template;
    //}


    private string ProcessForeach(string template, Dictionary<string, object> data)
    {
        var conditionPattern = @"{{#foreach\s+(\w+)\s+in\s+(\w+)}}([\s\S]*?){{/foreach}}";
        var matches = Regex.Matches(template, conditionPattern);
        if (matches.Count == 0)
        {
            return template;
        }

        foreach (Match match in matches)
        {
            string itemVar = match.Groups[1].Value;
            string enumerableVar = match.Groups[2].Value;
            string innerTemplate = match.Groups[3].Value;

            if (!data.ContainsKey(enumerableVar))
                throw new Exception($"Переменная перечисления '{enumerableVar}' не найдена в контексте.");

            if (!(data[enumerableVar] is IEnumerable enumerable))
                throw new Exception($"Переменная '{enumerableVar}' не является перечислением элементов.");

            var resultBuilder = new StringBuilder();

            // Обрабатываем каждый элемент в коллекции
            foreach (var item in enumerable)
            {
                // Для каждого элемента создаем копию шаблона
                string currentItemTemplate = innerTemplate;

                // Паттерн для замены свойств объекта
                var propPattern = $"{{{{\\s*{itemVar}\\.(\\w+)\\s*}}}}";
                var propMatches = Regex.Matches(currentItemTemplate, propPattern);

                foreach (Match propMatch in propMatches)
                {
                    string propName = propMatch.Groups[1].Value;
                    var itemType = item.GetType();
                    var propertyInfo = itemType.GetProperty(propName);

                    if (propertyInfo == null)
                        throw new Exception($"Свойство '{propName}' не найдено у объекта типа '{itemType.Name}'.");

                    var propertyValue = propertyInfo.GetValue(item);
                    currentItemTemplate = propertyValue == null
                            ? currentItemTemplate.Replace(propMatch.Value, "")
                            : currentItemTemplate.Replace(propMatch.Value, propertyValue.ToString() ?? "");
                }

                // Добавляем обработанный элемент в результат
                resultBuilder.Append(currentItemTemplate);
            }

            // Заменяем блок {{#foreach ...}} на все обработанные элементы
            template = template.Replace(match.Value, resultBuilder.ToString());
        }

        return template;
    }

}
    
