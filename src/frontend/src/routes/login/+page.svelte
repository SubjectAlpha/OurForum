<script lang="ts">

    import { login, token } from "../../hooks/auth";
	import Input from "../../components/form/input.svelte";
	import { TOKEN_NAME, EMAIL_REGEX, PASSWORD_REGEX } from "$lib/constants";

    let email = "admin@email.com";
    let password = "str0ngDevelopmentPassw0rd!";

    $: emailValid = true;
    $: passwordValid = true;
    $: token;

    if($token && window)
    {
        window.location.replace("/");
    }

    async function submitForm(){
        emailValid = EMAIL_REGEX.test(email);
        passwordValid = PASSWORD_REGEX.test(password);

        if(emailValid && passwordValid)
        {
            login(email, password).then(r => {
                if(r && localStorage){
                    localStorage.setItem(TOKEN_NAME, r);
                    window.location.replace("/");
                }
            })
            .catch(ex => console.log(ex));
        }
    }
</script>

<div class="p-12 pt-0 grid h-screen place-items-center">
	<form
		class="bg-white dark:bg-slate-800 shadow-md rounded p-8 mb-4 lg:w-1/3 md:w-1/3 sm:w-full"
		on:submit={submitForm}
	>
		<div class="mb-4">
            <Input
                id="emailInput"
                bind:value={email}
                type="email"
                labelText="Email Address"
                placeholder="john.smith@email.com"
                errorText="Please enter an email."
                validation={EMAIL_REGEX}
            />
		</div>
		<div class="mb-6">
            <Input
                id="passwordInput"
                bind:value={password}
                type="password"
                labelText="Password"
                placeholder= {"******************"}
                errorText="Please enter a password."
                validation={PASSWORD_REGEX}
            />
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
				href="any"
			>
				Forgot Password?
			</a>
		</div>
	</form>
</div>
