<script>
    import "../app.css";
    import Navbar from "../components/navbar.svelte";

    console.log(import.meta.env.VITE_TITLE);

    const title = import.meta.env.VITE_TITLE;
</script>

<div class="bg-white dark:bg-slate-800 h-screen overflow-y-hidden overflow-x-hidden">
    <Navbar title={title} />
    <slot />
</div>


