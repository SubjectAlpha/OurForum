#!/bin/sh

sh -c "bash -c \"cd frontend; npm run dev &\""
sh -c "bash -c \"cd backend; dotnet run start &\""