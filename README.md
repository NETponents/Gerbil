# Gerbil
## What is it?

[![Join the chat at https://gitter.im/NETponents/Gerbil](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/NETponents/Gerbil?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
Gerbil is an automated hacking tool powered by a trainable AI engine. It was originally designed to be an automated network penetration tester by scanning your network for weak points. When it does find a weak point, it will use previous experience to decide how to attack the server.

## Project State
Gerbil is still in it's early stages. Right now it's just a fancy network scanner. If you have experience in C#, feel free to contribute to help speed up development!

## System requirements
Doesn't matter if you have a Cray supercomputer or a potato. If it can run .NET 4.5, you are good to go.

## Download
To get Gerbil, `git clone` this repository to your hard drive. From there, open up *~/Gerbil/Gerbil.sln* Using Visual Studio 2012 or later. Compile the project using the **Release** build profile. After which, check the *~/Gerbil/bin/release* folder and run *gerbil.exe*.

Binary releases will be availible once we release a stable package of Gerbil.

## Use
Gerbil uses a custom CLI. See the project wiki on the right-hand side for CLI command usage.

## Obvious Legal Disclaimer
Since this is technically a "hacking tool", NETponents, @ARMmaster17, or any other contributers cannot be held liable for actions taken by this software on either your own or someone elses network. We reccommend testing Gerbil on a protected private lab network since Gerbil does posses tools to potentially wipe/modify network computer hard disks.
