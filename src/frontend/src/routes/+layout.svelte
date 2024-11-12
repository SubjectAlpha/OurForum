<script>
	import '../app.css';
	import Navbar from '../components/navbar.svelte';

	const title = import.meta.env.VITE_TITLE;
	console.log('title', title);
	$: title;
</script>

<div
	class="bg-white dark:bg-slate-800 dark:text-white
		h-screen overflow-y-hidden overflow-x-hidden"
>
	<Navbar {title} />
	<main class="bg-gray-50 dark:bg-slate-600 m-2 p-1 pb-10 lg:pb-14 h-full rounded overflow-y-auto">
		<slot />
	</main>
</div>
