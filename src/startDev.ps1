Start-Process pwsh '-c', 'cd backend; dotnet run start -lp "https"'
Start-Process pwsh '-c', 'cd frontend; npm run dev'