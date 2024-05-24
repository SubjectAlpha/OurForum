<script>
    import { PUBLIC_BACKEND_URL } from "$env/static/public";
    const emailRegex = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{12,}$/;

    let email = "";
    let password = "";

    $: emailValid = true;
    $: passwordValid = true;

    async function submitForm(e){
        e.preventDefault();
        emailValid = emailRegex.test(email);
        passwordValid = passwordRegex.test(password);

        if(emailValid && passwordValid)
        {
            console.log(`${PUBLIC_BACKEND_URL}/user/authenticate`);
            const res = await fetch(`${PUBLIC_BACKEND_URL}/user/authenticate`, {
                method: "POST",
                body: {
                    "email": email,
                    "password": password
                }
            });
            console.log(res);
        }
    }
</script>

<div class="p-12 pt-0 grid h-screen place-items-center">
	<form
		class="bg-white dark:bg-slate-600 shadow-md rounded p-8 mb-4 lg:w-1/3 md:w-1/3 sm:w-full"
		on:submit={submitForm}
	>
		<div class="mb-4">
			<label class="block text-gray-700 dark:text-white text-sm font-bold mb-2" for="email">
                Email Address
            </label>

            <input
                bind:value={email}
                class:border-red-500={!emailValid}
                class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                type="email"
                placeholder="john.smith@email.com"
            />
			<p class:hidden={emailValid} class="text-red-500 text-xs italic hidden">
				Please enter an email.
			</p>
		</div>
		<div class="mb-6">
            <label class="block text-gray-700 dark:text-white text-sm font-bold mb-2" for="password">
                Password
            </label>

            <input
                bind:value={password}
                class:border-red-500={!passwordValid}
                class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                type="password"
                placeholder={"******************"}
            />
			<p class:hidden={passwordValid} class="text-red-500 text-xs italic hidden">
				Please enter a password.
			</p>
		</div>
		<div class="flex items-center justify-between">
			<button
				class="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-outline"
				type="submit"
			>
				Sign In
			</button>
			<a
				class="inline-block align-baseline font-bold text-sm text-blue-500 hover:text-blue-800"
				href="#"
			>
				Forgot Password?
			</a>
		</div>
	</form>
</div>
