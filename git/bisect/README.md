# Bisect

A demonstration of how to use [bisect](https://git-scm.com/docs/git-bisect) in git.

Bisect can be useful when trying to determine when a bug or other behavior was introduced into a case base.  Bisect is useful for debugging the code to determine when it was introduced.

To use Bisect, you need to determine the last good revision and the first bad revision.

To demonstrate bisect, we will use a simple text file.  In real practice, you would likely have a test suite that you could run agains the code and it would tell you if the code is passing or not.  If the test suite passes then the current revision is good.  If it fails, then the current revision is bad.

To begin, first create a simple text file.  You can put whatever you want in this test file.  I am going to create a file called quotes.md.  This file will contains a collection of quotes.

## Step one - Create quotes.md

Using your favorite text editor, create a new file called quotes.md.
Then add a line to the file

```markdown
# A collection of quotes
```

## Step two - commit changes

Commit the current file to Git.

```shell
git add quotes.md
git commit -m "Initial commit"
```

## Step three - Add a quote or more

Now add some quotes to the quotes.md file.  Only add a few.

```markdown

> "Programming today is a race between software engineers striving to build bigger and better idiot-proof programs, and the Universe trying to produce bigger and better idiots. So far, the Universe is winning."
>
> -- Rick Cook, The Wizardry Compiled 

> “Always code as if the guy who ends up maintaining your code will be a violent psychopath who knows where you live”
>
> -- John Woods
```

And now commit these changes.

```shell
git add quotes.md
git commit -m "Add quotes"
```

## Step four - Add additional quote, but make a mistake

Now that you have some quotes in the file, we want to add some more pithy quotes to it.  However, this time we are going to make an intentional mistake.  In the following example, I have misspelled the last name.  Under real world conditions we would not intentionally make a mistake; However, this is simply for demostration purposes.

```markdown
> An investment in knowledge pays the best interest.
>
> -–  Benjamin Fraknlin
```

And now commit those updates.

```shell
git add quotes.md
git commit -m "Add quote.  Mispelled name"
```

## Step five - Add some additional quotes

Now that we have added some qutoes and made a mistake we shoud add some more quotes.  This is to provide a scenario where the change you made is able to pass any review or test and is now in production waiting to wreak havoc.

Let us add the quotes above and below the previous quote(s).

```markdown
> "Mathematicians deal with large numbers sometimes, but never in their income."
>
> -- Isaac Asimov, Prelude to Foundation 

> "Two things are infinite: the universe and human stupidity; and I'm not sure about the universe."
> -- Albert Einstein

> "Don’t walk in front of me... I may not follow
>
> Don’t walk behind me... I may not lead
>
> Walk beside me... just be my friend"
>
> -- Albert Camus 
```

And now commit the changes.

```shell
git add quotes.md
git commit -m "Add inspirational quotes"
```

## Step six - Use bisect to debug and find error

At some point it will be discovered that there is an error in the file.  This is when we can use bisect to find it.

First we will use ```git log --oneline``` to review the current state of the repo.

When I execute ```git log --oneline``` I get the following output.  Your output will have different SHA values and will represent the specific comments and changes you made in you repo.  The repo I am using has additional activity.  I am only showing the recent activity in the branch we have been working on for this example.

```
git log --oneline
e1cd28b (HEAD -> bisect) Add inspirational quotes
3c30afc Additional quote.  Misspelled name
8219120 Add quotes
603859f Initial commit
08e47b7 (main) Update challenge1.yaml
...
```

You can see in the comments that the error occurred in the ```3c30afc``` commit.  If only every bug was this simple to find.  I indicated in the comment that there was an error to make demostration of bisect easier.

In looking at the various commits above, we know that we know that the last good commit was ```08e47b7```.  The error was found after we committed ```e1cd28b```.  So the error must have been introduced somewhere between those two commits.  We will use git bisect to find where we introduced the mistake.

We begin the process by using the following command:

```shell
git bisect start
```

When I execute the ```git bisect start``` command I get the following error message.

> You need to run this command from the toplevel of the working tree.

This indicates that I need to be at the top of the repo.  If you are at the top level of your repo already then you will not get this error.

In my repo I need to move up the directory structure two levels.  Once I have done this I can again execute ```git bisect start``` and it will being the bisect process.

After entering the command it simply returns to the command line.  Now I can tell it where the first known bad commit is.  You can do this by specifying the SHA or leaving it blank to use the current HEAD.  (You can also use a tag or other identifying treeish method).

```shell
git bad     # Use the current HEAD
git bad e1cd28b # Use the commit e1cd28b
```

For demonstration purposes I will use a specific commit SHA.

```shell
git bisect bad e1cd28b
```

Now that we have specified the last known bad commit we can now specify the last known good commit.

```shell
git bisect good 08e47b7
```

After entering the last known bad and last known good commits you should get a message similar to the following:

> Bisecting: 1 revision left to test after this (roughly 1 step)
>
> [8219120c540e45f64e7f836a21c361b499446b83] Add quotes

At this point the repo is restored to the point indicated by the commit hash above.  The output is also indicating that we have one additional revision to test and this will take 1 step to complete.  Our example is a bit limited since we only have a few commits.  If we had a much broader range we were searching then the number of steps would be larger.

Now you can open the file to see if you can find the error.  If this were source code you would compile the project and run any tests to determine if it is working properly.  Since this is a simple text file we will just open the quotes.md file and see if we can find the error.

In this revision of the quotes.md file we should have two quotes.  The first is from Rick Cook and the second is from John Woods.  So it appears that this version of the file is a good version.

Execute

```shell
git bisect good # head is implied and is the version we just tested.
```

This will mark the current head as a good version and will return a new message.

> Bisecting: 0 revisions left to test after this (roughly 0 steps)
>
> [3c30afce10bc5e44b168e815c6414c17cf4e3a42] Additional quote.  Misspelled name

What the command has done is bisect the current search space between the original good and bad commits specified and checked out the next midpoint.

If we now open the quotes.md file we can see that the error is present as expected.  

Since this current revision contains the error we mark the current revision as bad.

```shell
git bisect bad # head is implied
```

If we had additional steps then the bisect command would continue to bisect the current search space and return the midpoint. In our case, there was no additional search space so after indicating that this is a bad revision we get the following output.

```
3c30afce10bc5e44b168e815c6414c17cf4e3a42 is the first bad commit
commit 3c30afce10bc5e44b168e815c6414c17cf4e3a42
Author: Robert Little <rll04747@pomona.edu>
Date:   Sat Jan 2 16:36:38 2021 -0800

    Additional quote.  Misspelled name

 git/bisect/quotes.md | 4 ++++
 1 file changed, 4 insertions(+)
 ```

This output is indicating that it found the error.  It is saying, here is the first bad commit.  The HEAD of the repo is now pointing the the commit revision 3c30afce and if we open the quotes.md file we can see that indeed the error is present.

At this point we have found where the error was first introduced.  you can now take whatever corrective measures are necessary.  If you are a QA engineer then it might be creating a new test to ensure that the introduced error is caught in the future, providing a report to the author so that they can correct it, or writing a patch.

## Step seven - Exit bisect

Now that we have found the error and performed whatever is necessary to correct it we can exit ```git bisect```.  To do this we simply execute ```git bisect reset```.  This will return us to the original location we were at when we began to use bisect.

# Conclusion

The bisect command can be a useful tool to find where a bug or error was introduced into a repo.
