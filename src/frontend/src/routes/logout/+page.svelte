<script lang="ts">
	import { token } from "../../hooks/auth";

    $:token;

    if($token && window){
        token.set("");
        window.location.replace("/");
    }
</script>