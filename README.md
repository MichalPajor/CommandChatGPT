# CommandChatGPT
Narzędzie konsolowe wyszukujące komendy używane w terminalach. Uzupełnij pytanie treścią szukanej komendy, a aplikacja zwróci ją oraz automatycznie skopiuje do schowka,
abyś mógł jej szybko użyć.


# 1. Utwórz konto na platformie openai.com

# 2. Wygeneruj swój APIKEY
  - User -> API keys -> Create new secret key

# 3. Wklej swój APIKEY.
Dodaj go do pliku Program.cs do pomiższej linii
```c#
client.DefaultRequestHeaders.Add("authorization", "Bearer TOKEN");
```
w miejsce "TOKEN".

# 4. (OPCJONALNIE) dodaj program do zmiennych środowiskowych systemu Windows dla lepszego użytkowania.
- Wygeneruj self-contained single file application file
```bash
dotnet publish -r win-x64
```
- dodaj ścieżkę do wygenerowanego pliku .exe do zmiennych środowiskowych (PATH)

# 5. Demo
![alt text](https://i.imgur.com/kTHk48W.png)
