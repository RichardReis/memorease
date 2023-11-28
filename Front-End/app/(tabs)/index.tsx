import {
  Dimensions,
  SafeAreaView,
  StyleSheet,
  Text,
  TouchableOpacity,
  View,
} from "react-native";
import TextStyles from "../../themedStyles/Text";
import HomeCardStyles from "../../themedStyles/HomeCard";
import Colors from "../../constants/Colors";
import Spacing from "../../constants/Spacing";
import Button from "../../components/Button";
import Icon from "../../components/Icon";
import { useRouter } from "expo-router";
import { useEffect, useState } from "react";
import { LoadHome, LoadHomeData } from "../../service/Home/loadHome";

type HomeCardProps = {
  label: string;
  value: number;
  type: "sucess" | "neutral" | "danger";
  // href: string;
};

export default function Home() {
  const router = useRouter();
  const [data, setData] = useState<LoadHomeData | null>(null);

  useEffect(() => {
    GetList();
  }, []);

  const GetList = async () => {
    let response = await LoadHome();
    if (response) setData(response);
  };

  const HomeCard = ({ label, value, type }: HomeCardProps) => {
    return (
      <View style={{ ...HomeCardStyles[type] }}>
        <Text style={{ ...HomeCardStyles.text }}>{label}</Text>
        <Text style={{ ...HomeCardStyles.textvalue }}>{value}</Text>
      </View>
    );
  };

  if (!data) return <></>;
  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <View style={{ flexDirection: "row", alignItems: "baseline" }}>
          <View style={{ flex: 1 }}>
            <Text style={{ ...TextStyles.titleHeader }}>
              {"Olá, "}
              {data.userFirstName}
            </Text>
            <Text style={{ ...TextStyles.subTitleHeader }}>
              {"Pronto para se tornar uma melhor versão de sí mesmo?"}
            </Text>
          </View>
          <View>
            <TouchableOpacity onPress={() => router.push("/userMenu")}>
              <Icon color="white" name="account-circle-outline" size={50} />
            </TouchableOpacity>
          </View>
        </View>
      </View>
      <View style={styles.content}>
        <Text style={{ ...TextStyles.regular, marginBottom: Spacing.g }}>
          {"Aqui está seu acompanhamento diario"}
        </Text>

        <View style={styles.cardGroup}>
          <HomeCard label="A Revisar" value={data.inReview} type="sucess" />
          <HomeCard
            label="Em Aprendizagem"
            value={data.inLearning}
            type="neutral"
          />
          <HomeCard
            label="Total Pendente"
            value={data.totalCount}
            type="danger"
          />
        </View>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors["light"].contentBackground,
  },
  header: {
    height: "30%",
    justifyContent: "flex-end",
    paddingVertical: Spacing.xg,
    paddingHorizontal: Spacing.g,

    backgroundColor: Colors["light"].primary,

    borderBottomRightRadius: Spacing.m,
    borderBottomLeftRadius: Spacing.m,
  },
  text: {
    fontSize: 16,
  },
  content: {
    flex: 1,
    padding: Spacing.xg,
    backgroundColor: Colors["light"].contentBackground,
  },
  cardGroup: {
    flexWrap: "wrap",
    gap: Spacing.g,
  },
});
