<script>
	// on validation failed

</script>

<div class="p-12">
	<form
		class="bg-white dark:bg-slate-600 shadow-md rounded px-8 pt-6 pb-8 mb-4"
		on:submit={(e) => {
			e.preventDefault();
            var email = document.getElementById("email").value;
            var password = document.getElementById("password").value;

            if(password == "" || password.length < 4)
            {
                document.getElementById('password')?.classList.toggle('border-red-500');
	            document.getElementById('password-validation')?.classList.toggle('hidden');
            }

            console.log(email);
		}}
	>
		<div class="mb-4">
			<label class="block text-gray-700 dark:text-white text-sm font-bold mb-2" for="email">
				Email
			</label>
			<input
				class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
				id="email"
				type="email"
				placeholder="Email"
			/>
		</div>
		<div class="mb-6">
			<label class="block text-gray-700 dark:text-white text-sm font-bold mb-2" for="password">
				Password
			</label>
			<input
				class="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 mb-3 leading-tight focus:outline-none focus:shadow-outline"
				id="password"
				type="password"
				placeholder="******************"
			/>
			<p id="password-validation" class="text-red-500 text-xs italic hidden">
				Please choose a password.
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
