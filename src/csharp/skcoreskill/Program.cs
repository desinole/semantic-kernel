using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.CoreSkills;



var myKernel = Kernel.Builder.Build();
myKernel.ImportSkill(new ConversationSummarySkill(myKernel), "Summarize");

const string ThePromptTemplate =@"
Take this scene from the movie Lord of the Rings: The Two Towers 



    Frodo: I can’t do this, Sam…

    Sam: I know! It’s all wrong!
    By rights we shouldn’t even be here.
    But we are.
    It’s like in the great stories Mr. Frodo.
    The ones that really mattered.
    Full of darkness and danger they were,
    and sometimes you didn’t want to know the end.
    Because how could the end be happy.
    How could the world go back to the way it was when so much bad happened.
    But in the end, it’s only a passing thing, this shadow.
    Even darkness must pass.
    A new day will come.
    And when the sun shines it will shine out the clearer.
    Those were the stories that stayed with you.
    That meant something.
    Even if you were too small to understand why.
    But I think, Mr. Frodo, I do understand.
    I know now.
    Folk in those stories had lots of chances of turning back only they didn’t.
    Because they were holding on to something.

    Frodo: What are we holding on to, Sam?

    Sam : That there’s some good in this world, Mr. Frodo. And it’s worth fighting for.

Answer the following questions:
1. Who is trying to encourage and whom?
2. Summarize what Sam is trying to say to Frodo in one sentence

";

var processPrompt = myKernel.CreateSemanticFunction(ThePromptTemplate, maxTokens: 150);

var answers = await processPrompt.InvokeAsync();
Console.WriteLine(answers);